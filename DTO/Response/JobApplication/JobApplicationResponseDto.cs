using TeqetariApi.Models;

namespace TeqetariApi.DTO.Response.JobApplication;

public record JobApplicationResponseBaseDto
{
    public required int Id { get; set; }
    public required int EmployeeId { get; set; }
    public required DateTime AppliedAt { get; set; }
    public required ApplicationStatus Status { get; set; }
}
public record EmployeeApplicationResponseDto : JobApplicationResponseBaseDto
{
    public required int JobPostId { get; set; }
    public required string JobTitle { get; set; }
    public string? CoverLetter { get; set; }
}
public record DirectHireRequestResponseDto : JobApplicationResponseBaseDto
{
    public required int EmployerId { get; set; }
    public required string JobTitle { get; set; }
    public required DateTime RequestedDate { get; set; }
    public required string DutyDescription { get; set; }
}