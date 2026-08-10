using TeqetariApi.Domain.Enums;
using TeqetariApi.Domain.Exceptions;
using System.Net.Mail;


namespace TeqetariApi.Domain.Models;

public class Employee
{

    private bool IsValidEmail(string email)
{
    try
    {
        var addr = new MailAddress(email);
        return addr.Address == email;
    }
    catch
    {
        return false;
    }
}
    public int Id { get; set; }
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
    
    private string? _email;
    public string? Email
    {
        get => _email;
    set
    {
        // Allow null or whitespace to mean "no email provided"
        if (string.IsNullOrWhiteSpace(value))
        {
            _email = null;
            return;
        }

        // Validate format ONLY if an actual email string was passed
        if (!IsValidEmail(value))
        {
            throw new InvalidModelFieldException("Email", value, "A valid email address is required.");
        }

        _email = value;
    }
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
    public required int YearsOfExperience
    {
        get;
        set => field = (value >= 0 && value <= 50)
            ? value
            : throw new ValueOutOfRangeException(
                nameof(YearsOfExperience),
                value,
                "Years of experience must be between 0 and 50.");
    }
    public decimal ExpectedSalary
    {
        get;
        set => field = (value >= 0)
            ? value
            : throw new ValueOutOfRangeException(
                nameof(ExpectedSalary),
                value,
                "Expected salary cannot be a negative amount.");
    }

    public int TotalApplicationCount => JobApplications?.Count ?? 0;
    public JobCategory JobCategory { get; set; }
    public List<string>? Skills { get; set; }
    public bool IsAvailable { get; set; } = true;
    public bool BackgroundCheckPassed { get; set; } = false;
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    public ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
    public ICollection<PlacementContract> PlacementContracts { get; set; } = new List<PlacementContract>();
}