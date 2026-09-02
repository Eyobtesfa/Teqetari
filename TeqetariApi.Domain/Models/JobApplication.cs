namespace TeqetariApi.Domain.Models;

public class JobApplication
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public int JobPostId { get; set; }
    public JobPost JobPost { get; set; } = null!;
    public string? CoverMessage { get; set; }
    public required DateTime AppliedAt { get; set; } = DateTime.UtcNow;
    public DateTime? RespondedAt { get; set; }
    public string? DeclineReason { get; set; }
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
    public PlacementContract? PlacementContract { get; set; }
}



public enum ApplicationStatus
{
    Pending = 1,
    Accepted = 2,
    Declined = 3
}