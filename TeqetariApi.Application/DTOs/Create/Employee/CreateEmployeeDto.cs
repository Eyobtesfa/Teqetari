using System.ComponentModel.DataAnnotations;
using TeqetariApi.Domain.Enums;


namespace TeqetariApi.Application.DTOs.Create.Employee;

public record CreateEmployeeDto
{

    public required string PhoneNumber { get; init; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string NationalIdNumber { get; init; }

    public string? Email { get; init; }

    public required string City { get; init; }

    public required string SubCity { get; init; }

    public required string Woreda { get; init; }

    public required int YearsOfExperience { get; init; }

    public required decimal ExpectedSalary { get; init; }

    public List<string>? Skills { get; init; }

    public required JobCategory JobCategory { get; init; }

}