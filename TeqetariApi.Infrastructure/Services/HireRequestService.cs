using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TeqetariApi.Application.DTOs.Create;
using TeqetariApi.Application.DTOs.Response;
using TeqetariApi.Application.DTOs.Update;
using TeqetariApi.Application.Interfaces;
using TeqetariApi.Domain.Models;
using TeqetariApi.Infrastructure.Persistence;
using TeqetariApi.Domain.Exceptions;

namespace TeqetariApi.Infrastructure.Services;

public class HireRequestService(
    TeqetariDbContext context,
    ILogger<HireRequestService> logger
) : IHireRequestService
{
    private const int HouseholdMaxActiveRequest = 3;

    public async Task<(bool Success, HireRequestResponseDto? Result, IEnumerable<string> Errors)> SendHireRequestAsync(string appUserId, HireRequestDto dto, CancellationToken ct)
    {
        var employer = await context.Employers.FirstOrDefaultAsync(e => e.AppUserId == appUserId, ct);
        if (employer is null)
        {
            logger.LogWarning("Sending hire request failed: no employer profile found for AppUserId {AppUserId}", appUserId);
            return (false, null, new[] { "Employer Profile not found" });
        }

        var employeeExists = await context.Employees.AnyAsync(e => e.Id == dto.EmployeeId, ct);
        if (!employeeExists)
        {
            return (false, null, new[] { "Employee not found" });
        }

        var pendingRequest = await context.HireRequests.AnyAsync(h => h.EmployerId == employer.Id && h.EmployeeId == dto.EmployeeId && h.Status == HireRequestStatus.Pending, ct);
        if (pendingRequest)
        {
            logger.LogWarning("Request already sent it is {Status}", HireRequestStatus.Pending);
            return (false, null, new[] { "A pending Request already exists" });
        }

        if (employer is Household)
        {
            var requestCount = await context.HireRequests.CountAsync(h => h.EmployerId == employer.Id && (h.Status == HireRequestStatus.Pending || h.Status == HireRequestStatus.Accepted), ct);
            if (requestCount >= HouseholdMaxActiveRequest)
            {
                return (false, null, new[] { $"Household employers can have at most {HouseholdMaxActiveRequest} active requests at a time" });
            }
        }
        if (dto.StartDateFrom > dto.StartDateTo)
            return (false, null, new[] { "Start date range is invalid." });

        var hireRequest = new HireRequest
        {
            EmployerId = employer.Id,
            EmployeeId = dto.EmployeeId,
            OfferedSalary = dto.OfferedSalary,
            Message = dto.Message,
            StartDateFrom = DateTime.SpecifyKind(dto.StartDateFrom, DateTimeKind.Utc),
            StartDateTo = DateTime.SpecifyKind(dto.StartDateTo, DateTimeKind.Utc),
            Status = HireRequestStatus.Pending,
            RequestedAt = DateTime.UtcNow
        };

        context.HireRequests.Add(hireRequest);
        await context.SaveChangesAsync(ct);

        logger.LogInformation("Employer {EmployerId} sent hire request to Employee {EmployeeId} (HireRequestId: {HireRequestId}).",
            employer.Id, dto.EmployeeId, hireRequest.Id);

        return (true, MapToResponseDto(hireRequest), Enumerable.Empty<string>());
    }

    public async Task<(bool Success, IEnumerable<HireRequestResponseDto> Result, IEnumerable<string> Errors)> GetSentHireRequestsAsync(
    string appUserId, CancellationToken ct)
    {
        var employer = await context.Employers.FirstOrDefaultAsync(e => e.AppUserId == appUserId, ct);
        if (employer is null)
            return (false, Enumerable.Empty<HireRequestResponseDto>(), new[] { "Employer profile not found." });

        var requests = await context.HireRequests
            .Where(hr => hr.EmployerId == employer.Id)
            .OrderByDescending(hr => hr.RequestedAt)
            .Select(hr => MapToResponseDtoStatic(hr))
            .ToListAsync(ct);

        return (true, requests, Enumerable.Empty<string>());
    }

    public async Task<(bool Success, IEnumerable<HireRequestResponseDto> Result, IEnumerable<string> Errors)> GetReceivedHireRequestsAsync(
        string appUserId, CancellationToken ct)
    {
        var employee = await context.Employees.FirstOrDefaultAsync(e => e.AppUserId == appUserId, ct);
        if (employee is null)
            return (false, Enumerable.Empty<HireRequestResponseDto>(), new[] { "Employee profile not found." });

        var requests = await context.HireRequests
            .Where(hr => hr.EmployeeId == employee.Id)
            .OrderByDescending(hr => hr.RequestedAt)
            .Select(hr => MapToResponseDtoStatic(hr))
            .ToListAsync(ct);

        return (true, requests, Enumerable.Empty<string>());
    }

    public async Task<(bool Success, string? Error)> RespondToHireRequestAsync(
        string appUserId, int hireRequestId, HireRequestUpdateDto dto, CancellationToken ct)
    {
        var employee = await context.Employees.FirstOrDefaultAsync(e => e.AppUserId == appUserId, ct);
        if (employee is null)
            return (false, "Employee profile not found.");

        var hireRequest = await context.HireRequests.FirstOrDefaultAsync(hr => hr.Id == hireRequestId, ct);
        if (hireRequest is null || hireRequest.EmployeeId != employee.Id)
            return (false, "Hire request not found.");

        if (hireRequest.Status != HireRequestStatus.Pending)
            return (false, "This request has already been responded to.");

        await using var transaction = await context.Database.BeginTransactionAsync(ct);

        hireRequest.RespondedAt = DateTime.UtcNow;

        if (!dto.Accept)
        {
            hireRequest.Status = HireRequestStatus.Declined;
            hireRequest.DeclineReason = dto.DeclineReason;
            await context.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);

            logger.LogInformation("Employee {EmployeeId} declined HireRequest {HireRequestId}.", employee.Id, hireRequestId);
            return (true, null);
        }

        if (dto.ChosenStartDate is null)
            return (false, "A start date must be chosen to accept.");

        var chosenStartUtc = DateTime.SpecifyKind(dto.ChosenStartDate.Value, DateTimeKind.Utc);

        if (chosenStartUtc < hireRequest.StartDateFrom || chosenStartUtc > hireRequest.StartDateTo)
            return (false, "Chosen start date must fall within the employer's proposed range.");

        DateTime? chosenEndUtc = null;
        if (dto.ChosenEndDate is not null)
        {
            chosenEndUtc = DateTime.SpecifyKind(dto.ChosenEndDate.Value, DateTimeKind.Utc);
            if (chosenEndUtc <= chosenStartUtc)
                return (false, "End date must be after the start date.");
        }

        var commission = dto.AgencyCommissionPercentage ?? 0;
        if (commission < 0 || commission > 100)
            return (false, "Agency commission percentage must be between 0 and 100.");

        hireRequest.Status = HireRequestStatus.Accepted;

        PlacementContract contract;
        try
        {
            contract = new PlacementContract
            {
                EmployerId = hireRequest.EmployerId,
                EmployeeId = hireRequest.EmployeeId,
                HireRequestId = hireRequest.Id,
                StartDate = chosenStartUtc,
                EndDate = chosenEndUtc, // may be null — fine, entity allows it now
                Salary = hireRequest.OfferedSalary,
                AgencyCommissionPercentage = commission,
                IsActive = true
            };
        }
        catch (Exception ex) when (ex is InvalidModelFieldException or ValueOutOfRangeException)
        {
            await transaction.RollbackAsync(ct);
            return (false, ex.Message);
        }

        context.PlacementContracts.Add(contract);

        try
        {
            await context.SaveChangesAsync(ct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to create PlacementContract for HireRequest {HireRequestId}.", hireRequestId);
            await transaction.RollbackAsync(ct);
            throw;
        }

        await transaction.CommitAsync(ct);

        logger.LogInformation(
            "Employee {EmployeeId} accepted HireRequest {HireRequestId}; PlacementContract {ContractId} created.",
            employee.Id, hireRequestId, contract.Id);

        return (true, null);
    }

    private static HireRequestResponseDto MapToResponseDto(HireRequest hr) => MapToResponseDtoStatic(hr);

    private static HireRequestResponseDto MapToResponseDtoStatic(HireRequest hr) => new()
    {
        Id = hr.Id,
        EmployerId = hr.EmployerId,
        EmployeeId = hr.EmployeeId,
        OfferedSalary = hr.OfferedSalary,
        Message = hr.Message,
        StartDateFrom = hr.StartDateFrom,
        StartDateTo = hr.StartDateTo,
        RequestedAt = hr.RequestedAt,
        RespondedAt = hr.RespondedAt,
        DeclineReason = hr.DeclineReason,
        Status = hr.Status
    };

}

