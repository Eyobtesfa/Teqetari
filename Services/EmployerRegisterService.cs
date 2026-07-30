using Microsoft.EntityFrameworkCore;
using TeqetariApi.Data;
using TeqetariApi.DTO.Create.Employer;
using TeqetariApi.DTO.Response.Employer;
using TeqetariApi.Models;

namespace TeqetariApi.Services;

public class EmployerRegisterService(TeqetariDbContext context, ILogger<EmployerRegisterService> logger) : IRegisterEmployeeService
{
    public async Task<EmployerBaseResponseDto> RegisterEmployerAsync(CreateEmployerDto create, CancellationToken ct)
    {

        logger.LogInformation("Attempting to register employer with Email: {Email}", create.Email);

        var exists = await context.Employers
            .AnyAsync(e => e.Email == create.Email || e.PhoneNumber == create.PhoneNumber, ct);

        if (exists)
        {
            logger.LogWarning("Employer registration failed: Email {Email} or Phone {PhoneNumber} already exists", create.Email, create.PhoneNumber);
            throw new InvalidOperationException("An employer with this Email or Phone number already exits.");
        }

        Employer employer = create switch
        {
            CreateHouseholdEmployerDto household => new Household
            {
                Email = household.Email,
                Type = household.EmployerType,
                PhoneNumber = household.PhoneNumber,
                City = household.City,
                SubCity = household.SubCity,
                Woreda = household.Woreda,
                SpecialInstruction = household.SpecialInstruction,

                FirstName = household.FirstName,
                LastName = household.LastName,
                NationalIdNumber = household.NationalIdNumber,
                NumberOfFamilyMembers = household.NumberOfFamilyMembers,
                HasPets = household.HasPets ?? false
            },

            CreateCompanyEmployerDto company => new PrivateCompany
            {
                Email = company.Email,
                Type = company.EmployerType,
                PhoneNumber = company.PhoneNumber,
                City = company.City,
                SubCity = company.SubCity,
                Woreda = company.Woreda,
                SpecialInstruction = company.SpecialInstruction,

                Industry = company.Industry,
                CompanyName = company.CompanyName,
                TradeLicenseNumber = company.TradeLicenseNumber,
                TaxRegistrationNumber = company.TaxRegistrationNumber,
                ContactPersonName = company.ContactPersonName,
                ContactPersonRole = company.ContactPersonRole,
                Size = company.CompanySize
            },

            CreateGovernmentEmployerDto government => new GovernmentOrganization
            {
                Email = government.Email,
                Type = government.EmployerType,
                PhoneNumber = government.PhoneNumber,
                City = government.City,
                SubCity = government.SubCity,
                Woreda = government.Woreda,
                SpecialInstruction = government.SpecialInstruction,

                OrganizationName = government.OrganizationName,
                Sector = government.Sector,
                Department = government.Department,
                AuthorizedOfficerName = government.AuthorizedOfficerName,
                OfficialLetterRefNumber = government.OfficialLetterRefNumber
            },

            _ => throw new ArgumentException("Unsupported employer payload type.", nameof(create))
        };

        context.Employers.Add(employer);
        await context.SaveChangesAsync(ct);
        logger.LogInformation("Registered Employer profile for {EmployerId} as {EmployerType}.", employer.Id, employer.Type);
        return MapToResponseDto(employer);


    }


    public async Task<EmployerBaseResponseDto?> GetEmployerByIdAsync(int id, CancellationToken ct)
    {
        logger.LogInformation("Fetching employer by ID: {EmployerId}", id);

        // Fetch entity with AsNoTracking for high read performance.
        // EF Core TPH automatically instantiates the correct derived type (Household, PrivateCompany, etc.).
        var employer = await context.Employers
            .Include(e => e.JobPosts)
            .Include(e => e.PlacementContracts)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id, ct);

        if (employer is null)
        {
            logger.LogWarning("Employer with ID {EmployerId} was not found.", id);
            return null;
        }

        return MapToResponseDto(employer);
    }


    private static EmployerBaseResponseDto MapToResponseDto(Employer employer) => employer switch
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
            JobPostsCount = h.JobPosts.Count,
            PlacementContractsCount = h.PlacementContracts.Count,


            FirstName = h.FirstName,
            LastName = h.LastName,
            NationalIdNumber = h.NationalIdNumber,
            NumberOfFamilyMembers = h.NumberOfFamilyMembers,
            HasPets = h.HasPets
        },

        PrivateCompany p => new PrivateCompanyResponseDto
        {
            Id = p.Id,
            Type = p.Type,
            Email = p.Email,
            PhoneNumber = p.PhoneNumber,
            City = p.City,
            SubCity = p.SubCity,
            Woreda = p.Woreda,
            SpecialInstruction = p.SpecialInstruction,
            JobPostsCount = p.JobPosts.Count,
            PlacementContractsCount = p.PlacementContracts.Count,

            Industry = p.Industry,
            CompanyName = p.CompanyName,
            TradeLicenseNumber = p.TradeLicenseNumber,
            TaxRegistrationNumber = p.TaxRegistrationNumber,
            ContactPersonName = p.ContactPersonName,
            ContactPersonRole = p.ContactPersonRole,
            Size = p.Size

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
            JobPostsCount = g.JobPosts.Count,
            PlacementContractsCount = g.PlacementContracts.Count,

            OrganizationName = g.OrganizationName,
            Sector = g.Sector,
            Department = g.Department,
            AuthorizedOfficerName = g.AuthorizedOfficerName,
            OfficialLetterRefNumber = g.OfficialLetterRefNumber
        },
        _ => throw new InvalidOperationException("Unknown entity type.")
    };
}