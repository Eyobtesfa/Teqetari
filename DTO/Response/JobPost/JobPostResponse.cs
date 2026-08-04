using TeqetariApi.Enums;

namespace TeqetariApi.DTO.Response.JobPost;

public record JobPostResponseDto
{
    public required int Id { get; set; }
    public required int EmployerId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required JobCategory Category { get; set; }
    public required List<string> RequiredSkills { get; set; }
    public required decimal OfferedSalaryMin { get; set; }
    public required decimal OfferedSalaryMax { get; set; }
    public bool AccommodationProvided { get; set; } = false;
    public required int MinimumExperienceYears { get; set; }
}