namespace TeqetariApi.Domain.Models;

public class HireRequest
{
    public int Id { get; set; }
    public int EmployerId { get; set; }
    public Employer Employer { get; set; } = null!;
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public DateTime RequestedAt { get; set; } = DateTime.Now;
    public HireRequestStatus Status { get; set; } = HireRequestStatus.Pending;
}
public enum HireRequestStatus
{
    Pending = 1,
    Accepted = 2,
    Declined = 3
}