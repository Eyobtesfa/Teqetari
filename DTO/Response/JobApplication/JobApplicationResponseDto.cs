using TeqetariApi.Models;

namespace TeqetariApi.DTO.Response.JobApplication;

public record JobApplicationResponseDto(
    int Id,
    int JobPostId,
    string JobTitle,
    int EmployerId,
    string EmployerName,
    int EmployeeId,
    string EmployeeName,
    DateTime AppliedAt,
    ApplicationStatus Status,
    string? CoverLetter,
    bool HasContract,
    int? PlacementContractId
);
