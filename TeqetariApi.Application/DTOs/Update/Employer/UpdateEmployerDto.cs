using TeqetariApi.Domain.Enums;

namespace TeqetariApi.Application.DTOs.Update.Employer;

public class UpdateEmployerDto
{
    public required string Email { get; init; }
    public required string PhoneNumber { get; init; }
    public required string City { get; init; }
    public required string SubCity { get; init; }
    public required string Woreda { get; init; }
    public List<string>? SpecialInstructions { get; set; }
}

public class UpdateHouseholdEmployerDto : UpdateEmployerDto
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string NationalIdNumber { get; init; }
    public required int NumberOfFamilyMembers { get; init; }
    public bool? HasPets { get; init; }
}
public class UpdateCompanyEmployerDto : UpdateEmployerDto
{
    public required string CompanyName { get; init; }
    public required string TradeLicenseNumber { get; init; }
    public required string TaxRegistrationNumber { get; init; }
    public required string ContactPersonName { get; init; }
    public required string ContactPersonRole { get; init; }
    public required CompanySize CompanySize { get; init; }
}

public class UpdateGovernmentEmployerDto : UpdateEmployerDto
{
    public required string OrganizationName { get; init; }
    public required GovernmentSector Sector { get; init; }
    public required string Department { get; init; }
    public required string AuthorizedOfficerName { get; init; }
    public required string OfficialLetterRefNumber { get; init; }
}
