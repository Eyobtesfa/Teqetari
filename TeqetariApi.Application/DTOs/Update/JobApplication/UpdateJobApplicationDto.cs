using TeqetariApi.Domain.Models;


namespace TeqetariApi.Application.DTOs.Update.JobApplication;

public record UpdateEmployeeApplicationDto
{
    public string? CoverLetter { get; set; }
}
public record UpdateDirectHireRequestDto
{
    public required string JobTitle { get; set; }
    public required DateTime RequestedDate { get; set; }
    public required string DutyDescription { get; set; }
}