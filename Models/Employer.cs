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
            : throw new ArgumentException("A valid email address is required.");
    }
    public required string PhoneNumber
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("Phone number cannot be empty.");
    }
    public required string City
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("City cannot be empty.");
    }
    public required string SubCity
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("SubCity cannot be empty.");
    }
    public required string Woreda
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("Woreda cannot be empty.");
    }

}


public class Household : Employer
{
    public required string FirstName
    {
        get;

        set => field = !string.IsNullOrWhiteSpace(value)
        ? value
        : throw new ArgumentException("First name cannot be whitespace.");
    }
    public required string LastName
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
        ? value
        : throw new ArgumentException("Last name cannot be whitespace.");
    }

    public required string NationalIdNumber
    {
        get;
        set => field = (!string.IsNullOrWhiteSpace(value) && value.Length >= 16)
            ? value
            : throw new ArgumentException("Invalid National ID Number");
    }
    public int NumberOfFamilyMembers
    {
        get;
        set => field = value > 0
        ? value
        : throw new ArgumentOutOfRangeException(nameof(value), "Family size must be at least 1.");
    }
    public bool HasPets { get; set; } = false;
    public string? SpecialInstruction { get; set; }

}

public class PrivateCompany : Employer
{
    public required IndustryType Industry { get; set; }
    public required string CompanyName
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("Company name cannot be empty.");
    }
    public required string TradeLicenseNumber
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("Trade license number cannot be empty.");
    }
    public required string TaxRegistrationNumber
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("Tax registration number (TIN) cannot be empty.");
    }
    public required string ContactPersonName
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("Contact person name cannot be empty.");
    }
    public required string ContactPersonRole
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("Contact person role cannot be empty.");
    }
    public required CompanySize Size { get; set; }

}

public class GovernmentOrganization : Employer
{
    public required string AgencyName
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("Agency name cannot be empty.");
    }
    public required GovernmentSector Sector { get; set; }
    public required string Department { get; set; }
    public required string AuthorizedOfficerName
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("Authorized officer name cannot be empty.");
    }
    public required string OfficialLetterRefNumber
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("Official letter reference number cannot be empty.");
    }
}
