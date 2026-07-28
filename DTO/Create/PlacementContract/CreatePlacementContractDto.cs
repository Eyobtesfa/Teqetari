
namespace TeqetariApi.DTO.Create.PlacementContract;

public record CreatePlacementContractDto
{
    public required int EmployerId { get; set; }
    public required int EmployeeId { get; set; }
    public required int JobPostId { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required decimal Salary { get; set; }
    public required decimal AgencyCommissionPercentage { get; set; }
}