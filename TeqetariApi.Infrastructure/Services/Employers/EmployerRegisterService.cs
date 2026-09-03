using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TeqetariApi.Application.Interfaces;
using TeqetariApi.Infrastructure.Persistence;
using TeqetariApi.Application.DTOs.Create.Employer;
using TeqetariApi.Application.DTOs.Response.Employer;
using TeqetariApi.Domain.Models;

namespace TeqetariApi.Infrastructure.Services.Employers;

public class EmployerService(TeqetariDbContext context, ILogger<EmployerService> logger) : IEmployerService
{
    public async Task<(bool Success, IEnumerable<EmployerBaseResponseDto> Result, IEnumerable<string> Errors)> GetEmployersAsync(CancellationToken ct)
    {
        var employers = await context.Employers
            .Include(e => e.JobPosts)
            .Include(e => e.PlacementContracts)
            .ToListAsync(ct);

        logger.LogInformation("Retrieved {Count} employers from the database.", employers.Count);

        return (true, employers.Select(MapToResponseDto), Enumerable.Empty<string>());
    }

    private static EmployerBaseResponseDto MapToResponseDto(Employer e) => e switch
    {
        Household h => new HouseholdResponseDto
        {
            Id = h.Id,
            Type = h.Type,
            Email = h.Email,
            PhoneNumber = h.PhoneNumber,
            City = h.City,
            SubCity = h.SubCity,
            Woreda = h.Woreda,
            SpecialInstruction = h.SpecialInstruction,
            JobPostsCount = h.JobPosts?.Count ?? 0,
            PlacementContractsCount = h.PlacementContracts?.Count ?? 0,
            FirstName = h.FirstName,
            LastName = h.LastName,
            NationalIdNumber = h.NationalIdNumber,
            NumberOfFamilyMembers = h.NumberOfFamilyMembers,
            HasPets = h.HasPets
        },
        PrivateCompany c => new PrivateCompanyResponseDto
        {
            Id = c.Id,
            Type = c.Type,
            Email = c.Email,
            PhoneNumber = c.PhoneNumber,
            City = c.City,
            SubCity = c.SubCity,
            Woreda = c.Woreda,
            SpecialInstruction = c.SpecialInstruction,
            JobPostsCount = c.JobPosts?.Count ?? 0,
            PlacementContractsCount = c.PlacementContracts?.Count ?? 0,
            Industry = c.Industry,
            CompanyName = c.CompanyName,
            TradeLicenseNumber = c.TradeLicenseNumber,
            TaxRegistrationNumber = c.TaxRegistrationNumber,
            ContactPersonName = c.ContactPersonName,
            ContactPersonRole = c.ContactPersonRole,
            Size = c.Size
        },
        GovernmentOrganization g => new GovernmentOrganizationResponseDto
        {
            Id = g.Id,
            Type = g.Type,
            Email = g.Email,
            PhoneNumber = g.PhoneNumber,
            City = g.City,
            SubCity = g.SubCity,
            Woreda = g.Woreda,
            SpecialInstruction = g.SpecialInstruction,
            JobPostsCount = g.JobPosts?.Count ?? 0,
            PlacementContractsCount = g.PlacementContracts?.Count ?? 0,
            OrganizationName = g.OrganizationName,
            Sector = g.Sector,
            Department = g.Department,
            AuthorizedOfficerName = g.AuthorizedOfficerName,
            OfficialLetterRefNumber = g.OfficialLetterRefNumber
        },
        _ => throw new InvalidOperationException($"Unsupported employer type: {e.GetType().Name}")
    };

}