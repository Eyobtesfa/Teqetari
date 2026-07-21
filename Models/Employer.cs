using TeqetariApi.Enums;

namespace TeqetariApi.Models;

public class Employer
{
    public int Id { get; set; }
    public required EmployerType Type { get; set; }
    public required string Email
    {
        get;
        set => field = (!string.IsNullOrWhiteSpace(value) && value.Contains("@"))
            ? value
            : throw new InvalidModelFieldException(
                nameof(Email),
                value ?? "null",
                "A valid email address is required.");
    }
    public required string PhoneNumber
    {
        get;
        set => field = (!string.IsNullOrWhiteSpace(value) && value.Length == 10)
            ? value
            : throw new InvalidModelFieldException(
                nameof(PhoneNumber),
                value ?? string.Empty,
                "Phone number must be exactly 10 digits.");
    }
    public required string City
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new InvalidModelFieldException(
                nameof(City),
                value ?? string.Empty,
                "City cannot be empty.");
    }
    public required string SubCity
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new InvalidModelFieldException(
                nameof(SubCity),
                value ?? string.Empty,
                "SubCity cannot be empty.");
    }
    public required string Woreda
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new InvalidModelFieldException(
                nameof(Woreda),
                value ?? string.Empty,
                "Woreda cannot be empty.");
    }
    public List<string>? SpecialInstruction { get; set; }
    public ICollection<JobPost> JobPosts { get; set; } = new List<JobPost>();

}


public class Household : Employer
{
    public required string FirstName
    {
        get;

        set => field = !string.IsNullOrWhiteSpace(value)
        ? value
        : throw new InvalidModelFieldException(
            nameof(FirstName),
            value ?? string.Empty,
            "First name cannot be whitespace.");
    }
    public required string LastName
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
        ? value
        : throw new InvalidModelFieldException(
            nameof(LastName),
            value ?? string.Empty,
            "Last name cannot be whitespace.");
    }

    public required string NationalIdNumber
    {
        get;
        set => field = (!string.IsNullOrWhiteSpace(value) && value.Length >= 16)
            ? value
            : throw new InvalidModelFieldException(
                nameof(NationalIdNumber),
                value ?? string.Empty,
                "Invalid National ID Number");
    }
    public int NumberOfFamilyMembers
    {
        get;
        set => field = value > 0
        ? value
        : throw new ValueOutOfRangeException(
            nameof(NumberOfFamilyMembers),
            value,
            "Family size must be at least 1.");
    }
    public bool HasPets { get; set; } = false;


}

public class PrivateCompany : Employer
{
    public required IndustryType Industry { get; set; }
    public required string CompanyName
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new InvalidModelFieldException(
                nameof(CompanyName),
                value ?? string.Empty,
                "Company name cannot be empty.");
    }
    public required string TradeLicenseNumber
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new InvalidModelFieldException(
                nameof(TradeLicenseNumber),
                value ?? string.Empty,
                "Trade license number cannot be empty.");
    }
    public required string TaxRegistrationNumber
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new InvalidModelFieldException(
                nameof(TaxRegistrationNumber),
                value ?? string.Empty,
                "Tax registration number (TIN) cannot be empty.");
    }
    public required string ContactPersonName
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new InvalidModelFieldException(
                nameof(ContactPersonName),
                value ?? string.Empty,
                "Contact person name cannot be empty.");
    }
    public required string ContactPersonRole
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new InvalidModelFieldException(
                nameof(ContactPersonRole),
                value ?? string.Empty,
                "Contact person role cannot be empty.");
    }
    public required CompanySize Size { get; set; }

}

public class GovernmentOrganization : Employer
{
    public required string OrganizationName
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new InvalidModelFieldException(
                nameof(OrganizationName),
                value ?? string.Empty,
                "Organization name cannot be empty.");
    }
    public required GovernmentSector Sector { get; set; }
    public required string Department { get; set; }
    public required string AuthorizedOfficerName
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new InvalidModelFieldException(
                nameof(AuthorizedOfficerName),
                value ?? string.Empty,
                "Authorized officer name cannot be empty.");
    }
    public required string OfficialLetterRefNumber
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new InvalidModelFieldException(
                nameof(OfficialLetterRefNumber),
                value ?? string.Empty,
                "Official letter reference number cannot be empty.");
    }
}
