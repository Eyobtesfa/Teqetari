namespace TeqetariApi.Application.DTOs.Response.PlacementContract;

public record PlacementContractResponseDto(
    int Id,
    int EmployerId,
    string EmployerName,
    int EmployeeId,
    string EmployeeName,
    int JobPostId,
    string JobTitle,
    DateTime StartDate,
    DateTime EndDate,
    decimal Salary,
    decimal AgencyCommissionPercentage,
    decimal CalculatedCommissionAmount, // Helper field: (Salary * CommissionPercentage / 100)
    bool IsActive
);
