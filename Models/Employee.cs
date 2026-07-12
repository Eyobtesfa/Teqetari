using TeqetariApi.Enums;


namespace TeqetariApi.Models;

public class Employee
{
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
    public string? Email
    {
        get;
        set => field = (!string.IsNullOrWhiteSpace(value) && value.Contains("@"))
            ? value
            : throw new InvalidModelFieldException(
                nameof(Email),
                value ?? string.Empty,
                "A valid email address is required.");
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
    public int YearsOfExperience
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
    public required SkillCategory Skills { get; set; }
    public bool IsAvailable { get; set; } = true;
    public bool BackgroundCheckPassed { get; set; } = false;
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
}