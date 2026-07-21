using System.ComponentModel.DataAnnotations;
using TeqetariApi.Enums;


namespace TeqetariApi.DTO;

public record CreateEmployeeDto
{
    [Required]
    [Phone]
    public required string PhoneNumber { get; init; }
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string FirstName { get; init; }
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string LastName { get; init; }
    [Required]
    [StringLength(16, MinimumLength = 16)]
    public required string NationalIdNumber { get; init; }
    [EmailAddress]
    public string? Email { get; init; }
    [Required]
    public required string City { get; init; }
    [Required]
    public required string SubCity { get; init; }
    [Required]
    public required string Woreda { get; init; }
    [Required]
    [Range(0, 50, ErrorMessage = "Years of experience must be between 0 and 50.")]
    public required int YearsOfExperience { get; init; }
    [Required]
    [Range(0, 1000000, ErrorMessage = "Expected salary must be a positive value.")]
    public required decimal ExpectedSalary { get; init; }
    [Required]
    public List<string>? Skills { get; init; }
    [Required]
    public required JobCategory JobCategory { get; init; }

}