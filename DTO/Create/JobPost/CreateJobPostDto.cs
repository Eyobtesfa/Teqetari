using TeqetariApi.Enums;


namespace TeqetariApi.DTO.Create.JobPost;

public record CreateJobPostDto
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required JobCategory Category { get; init; }
    public required List<string> RequiredSkills { get; init; }
    public required decimal OfferedSalary { get; init; }
    public required string Location { get; init; }
    public required DateTime ExpirationDate { get; init; }
}