using System.Text.Json.Serialization;
using TeqetariApi.Domain.Enums;

namespace TeqetariApi.Application.DTOs.Response.Employer;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(HouseholdResponseDto), typeDiscriminator: "Household")]
[JsonDerivedType(typeof(PrivateCompanyResponseDto), typeDiscriminator: "PrivateCompany")]
[JsonDerivedType(typeof(GovernmentOrganizationResponseDto), typeDiscriminator: "GovernmentOrganization")]
public abstract record EmployerBaseResponseDto
{
    public required int Id { get; init; }
    public required EmployerType Type { get; init; }
    public required string Email { get; init; }
    public required string PhoneNumber { get; init; }
    public required string City { get; init; }
    public required string SubCity { get; init; }
    public required string Woreda { get; init; }
    public List<string>? SpecialInstruction { get; init; }
    public int JobPostsCount { get; init; }
    public int PlacementContractsCount { get; init; }
}

public record HouseholdResponseDto : EmployerBaseResponseDto
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string FullName => $"{FirstName} {LastName}".Trim();
    public required string NationalIdNumber { get; init; }
    public int NumberOfFamilyMembers { get; init; }
    public bool HasPets { get; init; }
}

public record PrivateCompanyResponseDto : EmployerBaseResponseDto
{
    public IndustryType Industry { get; init; }
    public required string CompanyName { get; init; }
    public required string TradeLicenseNumber { get; init; }
    public string? TaxRegistrationNumber { get; init; }
    public required string ContactPersonName { get; init; }
    public required string ContactPersonRole { get; init; }
    public CompanySize Size { get; init; }
}

public record GovernmentOrganizationResponseDto : EmployerBaseResponseDto
{
    public required string OrganizationName { get; init; }
    public GovernmentSector Sector { get; init; }
    public required string Department { get; init; }
    public required string AuthorizedOfficerName { get; init; }
    public required string OfficialLetterRefNumber { get; init; }
}