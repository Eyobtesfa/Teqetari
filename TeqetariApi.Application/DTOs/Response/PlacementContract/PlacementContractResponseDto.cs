namespace TeqetariApi.Application.DTOs.Response.PlacementContract;

public record PlacementContractResponseDto
{
    public required int Id { get; set; }
    public required int EmployerId { get; set; }
    public required int EmployeeId { get; set; }
    public required DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public required decimal Salary { get; set; }
    public required decimal AgencyCommissionPercentage { get; set; }
    public required bool IsActive { get; set; }
}
