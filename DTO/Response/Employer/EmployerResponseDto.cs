using TeqetariApi.Enums;

namespace TeqetariApi.DTO.Response.Employer;


public abstract record EmployerBaseResponseDto(
    int Id,
    EmployerType Type,
    string Email,
    string PhoneNumber,
    string City,
    string SubCity,
    string Woreda,
    List<string> SpecialInstruction,
    int JobPostsCount,
    int PlacementContractsCount
);


public record HouseholdResponseDto(
    int Id, EmployerType Type, string Email, string PhoneNumber, string City, string SubCity, string Woreda, List<string> SpecialInstruction, int JobPostsCount, int PlacementContractsCount,
    string FirstName,
    string LastName,
    string FullName,
    string NationalIdNumber,
    int NumberOfFamilyMembers,
    bool HasPets
) : EmployerBaseResponseDto(Id, Type, Email, PhoneNumber, City, SubCity, Woreda, SpecialInstruction, JobPostsCount, PlacementContractsCount);


public record PrivateCompanyResponseDto(
    int Id, EmployerType Type, string Email, string PhoneNumber, string City, string SubCity, string Woreda, List<string> SpecialInstruction, int JobPostsCount, int PlacementContractsCount,
    IndustryType Industry,
    string CompanyName,
    string TradeLicenseNumber,
    string TaxRegistrationNumber,
    string ContactPersonName,
    string ContactPersonRole,
    CompanySize Size
) : EmployerBaseResponseDto(Id, Type, Email, PhoneNumber, City, SubCity, Woreda, SpecialInstruction, JobPostsCount, PlacementContractsCount);


public record GovernmentOrganizationResponseDto(
    int Id, EmployerType Type, string Email, string PhoneNumber, string City, string SubCity, string Woreda, List<string> SpecialInstruction, int JobPostsCount, int PlacementContractsCount,
    string OrganizationName,
    GovernmentSector Sector,
    string Department,
    string AuthorizedOfficerName,
    string OfficialLetterRefNumber
) : EmployerBaseResponseDto(Id, Type, Email, PhoneNumber, City, SubCity, Woreda, SpecialInstruction, JobPostsCount, PlacementContractsCount);
