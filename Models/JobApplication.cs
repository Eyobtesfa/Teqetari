namespace TeqetariApi.Models;

public class JobApplication
{
    public int Id { get; set; }
    public int JobPostId { get; set; }
    public JobPost JobPost { get; set; } = null!;
    public int EmployeeId { get; set; }

    public required DateTime AppliedAt { get; set; } = DateTime.UtcNow;
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
    public string? CoverLetter { get; set; }


    public Employee Employee { get; set; } = null!;
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