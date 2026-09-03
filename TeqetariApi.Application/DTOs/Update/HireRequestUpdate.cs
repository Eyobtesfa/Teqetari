namespace TeqetariApi.Application.DTOs.Update;

public record HireRequestUpdateDto
{
    public required bool Accept { get; set; }
    public DateTime? ChosenStartDate { get; set; }
    public DateTime? ChosenEndDate { get; set; }              // optional
    public decimal? AgencyCommissionPercentage { get; set; }
    public string? DeclineReason { get; set; }
}