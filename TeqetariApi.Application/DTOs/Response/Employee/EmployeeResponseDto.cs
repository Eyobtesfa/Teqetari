using TeqetariApi.Domain.Enums;

namespace TeqetariApi.Application.DTOs.Response.Employee;

public record CreateEmployeeResponseDto
{
    public required int Id { get; init; }
    public required string PhoneNumber { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string NationalIdNumber { get; init; }
    public required string City { get; init; }
    public required string SubCity { get; init; }
    public required string Woreda { get; init; }
    public required int YearsOfExperience { get; init; }
    public required decimal ExpectedSalary { get; init; }
    public required JobCategory JobCategory { get; init; }
    public required List<string> Skills { get; init; } = new();
    public required bool IsAvailable { get; init; }
    public required bool BackgroundCheckPassed { get; init; }
    public required DateTime RegisteredAt { get; init; }
    public required int TotalApplicationCount { get; init; }
}