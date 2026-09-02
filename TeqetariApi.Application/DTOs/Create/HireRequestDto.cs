namespace TeqetariApi.Application.DTOs.Create;

public record HireRequestDto
{
    public required int EmployeeId { get; init; }
    public required decimal OfferedSalary { get; set; }
    public string? Message { get; set; }
    public required DateTime StartDateFrom { get; set; }
    public required DateTime StartDateTo { get; set; }

}