using TeqetariApi.Enums;

namespace TeqetariApi.DTO.Response.JobPost;

public record JobPostCreateDto(
    int Id,
    int EmployerId,
    string EmployerName,
    string Title,
    string Description,
    JobCategory Category,
    List<string> RequiredSkills,
    decimal OfferedSalary,
    string Location,
    DateTime ExpirationDate,
    bool IsActive,
    DateTime PostedAt,
    DateTime CreatedAt,
    int ApplicationsCount
);