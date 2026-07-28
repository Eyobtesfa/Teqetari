
namespace TeqetariApi.DTO.Create.JobApplication;

public record CreateJobApplicationDto
{
    public required int JobPostId { get; set; }
    public required int EmployeeId { get; set; }
    public string? CoverLetter { get; set; }
}