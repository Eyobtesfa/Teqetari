namespace TeqetariApi.DTO.Update.PlacementContract;

public record UpdatePlacementContractDto
{
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required decimal Salary { get; set; }
    public required decimal AgencyCommissionPercentage { get; set; }
    public required bool IsActive { get; set; }
}