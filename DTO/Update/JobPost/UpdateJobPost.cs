using TeqetariApi.Enums;


namespace TeqetariApi.DTO.Update.JobPost;

public record UpdateJobPostDto
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required JobCategory Category { get; init; }
    public required decimal OfferedSalaryMin { get; init; }
    public required decimal OfferedSalaryMax { get; init; }
    public bool AccommodationProvided { get; init; } = false;
    public int MinimumExperienceYears { get; init; } = 0;
    public required List<string> RequiredSkills { get; init; }
    public required string Location { get; init; }
    public required DateTime ExpirationDate { get; init; }
    public required bool IsActive { get; init; }
}