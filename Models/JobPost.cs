using TeqetariApi.Enums;
namespace TeqetariApi.Models;

public class JobPost
{
    public int Id { get; set; }
    public int EmployerId { get; set; }
    public Employer Employer { get; set; } = null!;
    public required string Title
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new InvalidModelFieldException(
                nameof(Title),
                value ?? string.Empty,
                "Title cannot be empty.");
    }
    public required string Description
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new InvalidModelFieldException(
                nameof(Description),
                value ?? string.Empty,
                "Description cannot be empty.");
    }
    public required JobCategory Category { get; set; }
    public required List<string> RequiredSkills { get; set; }
    public required decimal OfferedSalaryMin
    {
        get;
        set => field = (value >= 0)
            ? value
            : throw new ValueOutOfRangeException(
                nameof(OfferedSalaryMin),
                value,
                "Offered salary cannot be a negative amount.");
    }
    public required decimal OfferedSalaryMax
    {
        get;
        set => field = (value >= OfferedSalaryMin)
            ? value
            : throw new ValueOutOfRangeException(
                nameof(OfferedSalaryMax),
                value,
                "Offered salary max cannot be less than offered salary min.");
    }
    public bool AccommodationProvided { get; set; } = false;
    public int MinimumExperienceYears
    {
        get;
        set => field = (value >= 0)
                ? value
                : throw new ValueOutOfRangeException(
                    nameof(MinimumExperienceYears),
                    value,
                    "Minimum experience years cannot be a negative amount.");
    }

    public required string Location { get; set; }
    public bool IsActive { get; set; } = true;
    public required DateTime PostedAt { get; set; } = DateTime.UtcNow;
    public required DateTime ExpirationDate
    {
        get;
        set => field = (value > DateTime.UtcNow)
            ? value
            : throw new ValueOutOfRangeException(
                nameof(ExpirationDate),
                value,
                "Expiration date must be in the future.");
    }

    public ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();

}