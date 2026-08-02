using TeqetariApi.Enums;
using System.Text.Json.Serialization;


namespace TeqetariApi.DTO.Create.Employer;


[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(CreateHouseholdEmployerDto), typeDiscriminator: "Household")]
[JsonDerivedType(typeof(CreateCompanyEmployerDto), typeDiscriminator: "PrivateCompany")]
[JsonDerivedType(typeof(CreateGovernmentEmployerDto), typeDiscriminator: "GovernmentOrganization")]
public record CreateEmployerDto
{
    public required EmployerType EmployerType { get; init; }
    public required string Email { get; init; }
    public required string PhoneNumber { get; init; }
    public required string City { get; init; }
    public required string SubCity { get; init; }
    public required string Woreda { get; init; }
    public List<string>? SpecialInstruction { get; init; }
}

public record CreateHouseholdEmployerDto : CreateEmployerDto
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string NationalIdNumber { get; init; }
    public required int NumberOfFamilyMembers { get; init; }
    public bool? HasPets { get; init; }


}

public record CreateCompanyEmployerDto : CreateEmployerDto
{
    public required IndustryType Industry { get; init; }
    public required string CompanyName { get; init; }
    public required string TradeLicenseNumber { get; init; }
    public required string TaxRegistrationNumber { get; init; }
    public required string ContactPersonName { get; init; }
    public required string ContactPersonRole { get; init; }
    public required CompanySize CompanySize { get; init; }

}

public record CreateGovernmentEmployerDto : CreateEmployerDto
{
    public required string OrganizationName { get; init; }
    public required GovernmentSector Sector { get; init; }
    public required string Department { get; init; }
    public required string AuthorizedOfficerName { get; init; }
    public required string OfficialLetterRefNumber { get; init; }
}