using TeqetariApi.Models;


namespace TeqetariApi.DTO.Update.JobApplication;

public record UpdateJobApplicationDto
{
    public string? CoverLetter { get; set; }
    public ApplicationStatus? Status { get; set; }
}