using TeqetariApi.Domain.Models;

namespace TeqetariApi.Application.DTOs.Response;

public record HireRequestResponseDto
{
    public required int Id { get; set; }
    public required int EmployerId { get; set; }
    public required int EmployeeId { get; set; }
    public required decimal OfferedSalary { get; set; }
    public string? Message { get; set; }
    public required DateTime StartDateFrom { get; set; }
    public required DateTime StartDateTo { get; set; }
    public required DateTime RequestedAt { get; set; }
    public DateTime? RespondedAt { get; set; }
    public string? DeclineReason { get; set; }
    public required HireRequestStatus Status { get; set; }
}