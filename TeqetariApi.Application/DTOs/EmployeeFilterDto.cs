using TeqetariApi.Domain.Enums;

namespace TeqetariApi.Application.DTOs;

public record EmployeeFilterDto
{
    public JobCategory? Category { get; set; }
    public string? City { get; set; }
    public int? MinYearsOfExperience { get; set; }
    public decimal? MaxExpectedSalary { get; set; }
    public decimal? MinExpectedSalary { get; set; }
}