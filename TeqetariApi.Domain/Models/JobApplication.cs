namespace TeqetariApi.Domain.Models;

public class JobApplication
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public required DateTime AppliedAt { get; set; } = DateTime.UtcNow;
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
    public PlacementContract? PlacementContract { get; set; }
}


public class EmployeeApplication : JobApplication
{
    public int JobPostId { get; set; }
    public JobPost JobPost { get; set; } = null!;
    public string? CoverLetter { get; set; }
}

public class DirectHireRequest : JobApplication
{
    public int EmployerId { get; set; }
    public required Employer Employer { get; set; }
    public required string JobTitle { get; set; }
    public DateTime RequestedDate { get; set; }
    public required string DutyDescription { get; set; }
}
public enum ApplicationStatus
{
    Pending = 1,
    Accepted = 2,
    Rejected = 3,
    Withdrawn = 4,
    Shortlisted = 5,
    InterviewScheduled = 6,
}