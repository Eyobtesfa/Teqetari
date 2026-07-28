using TeqetariApi.Enums;

namespace TeqetariApi.DTO.Response.Employee;

public record CreateEmployeeResponseDto(
    int Id,
    string PhoneNumber,
    string FirstName,
    string LastName,
    string NationalIdNumber,
    string City,
    string SubCity,
    string Woreda,
    int YearsOfExperience,
    decimal ExpectedSalary,
    JobCategory JobCategory,
    List<string> Skills,
    bool IsAvailable,
    bool BackgroundCheckPassed,
    DateTime RegisteredAt,
    int TotalApplicationCount
);