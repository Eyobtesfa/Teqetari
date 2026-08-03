using TeqetariApi.DTO.Create.Employee;
using TeqetariApi.DTO.Response.Employee;

namespace TeqetariApi.Services.Employees;

public interface IRegisterEmployeeService
{
    Task<CreateEmployeeResponseDto> RegisterEmployeeAsync(CreateEmployeeDto create, CancellationToken ct);
    Task<CreateEmployeeResponseDto?> GetEmployeeByIdAsync(int id, CancellationToken ct);
    Task<bool> EmployeeExistsAsync(string nationalId, CancellationToken ct);
}