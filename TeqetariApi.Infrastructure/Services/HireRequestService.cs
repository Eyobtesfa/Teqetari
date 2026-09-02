using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TeqetariApi.Application.DTOs.Create;
using TeqetariApi.Application.DTOs.Response;
using TeqetariApi.Application.Interfaces;
using TeqetariApi.Domain.Models;
using TeqetariApi.Infrastructure.Persistence;

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

