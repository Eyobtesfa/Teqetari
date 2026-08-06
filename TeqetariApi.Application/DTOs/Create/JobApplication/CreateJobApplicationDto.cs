
namespace TeqetariApi.Application.DTOs.Create.JobApplication;

public record CreateJobApplicationBaseDto
{

    public required int EmployeeId { get; set; }

}

public record CreateEmployeeApplicationDto : CreateJobApplicationBaseDto
{
    public required int JobPostId { get; set; }
    public string? CoverLetter { get; set; }
}

public record CreateDirectHireRequestDto : CreateJobApplicationBaseDto
{
    public required int EmployerId { get; set; }
    public required string JobTitle { get; set; }
    public DateTime RequestedDate { get; set; } = DateTime.UtcNow;
    public required string DutyDescription { get; set; }
}