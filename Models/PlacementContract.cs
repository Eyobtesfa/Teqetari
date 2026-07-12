namespace TeqetariApi.Models;

public class PlacementContract
{
    public int Id { get; set; }
    public int EmployerId { get; set; }
    public int EmployeeId { get; set; }
    public Employer Employer { get; set; } = null!;
    public Employee Employee { get; set; } = null!;
    public required DateTime StartDate { get; set; } = DateTime.UtcNow;
    public required DateTime EndDate
    {
        get;
        set => field = (value > StartDate)
            ? value
            : throw new InvalidModelFieldException(
                nameof(EndDate),
                value,
                "End date must be after the start date.");
    }
    public required decimal Salary
    {
        get;
        set => field = (value >= 0)
            ? value
            : throw new ValueOutOfRangeException(
                nameof(Salary),
                value,
                "Salary cannot be a negative amount.");
    }
    public decimal AgencyCommissionPercentage
    {
        get;
        set => field = (value >= 0 && value <= 100)
            ? value
            : throw new ValueOutOfRangeException(
                nameof(AgencyCommissionPercentage),
                value,
                "Agency commission percentage must be between 0 and 100.");
    }
    public bool IsActive { get; set; } = true;
}