using TeqetariApi.Domain.Enums;
using TeqetariApi.Domain.Models;

namespace TeqetariApi.Application.DTOs.Create.JobPost;

public record CreateJobPostDto
{

    public required string Title { get; init; }
    public required string Description { get; init; }
    public required JobCategory Category { get; init; }
    public required decimal OfferedSalaryMin { get; init; }
    public required decimal OfferedSalaryMax { get; init; }
    public required List<string> RequiredSkills { get; init; }
    public required string Location { get; init; }
    public required WorkMode WorkMode { get; init; }
    public int MinimumExperienceYears { get; init; } = 0;
    public required DateTime ExpirationDate { get; init; }
}