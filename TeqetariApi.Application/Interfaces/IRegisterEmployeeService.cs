using TeqetariApi.Application.DTOs.Create.Employee;
using TeqetariApi.Application.DTOs.Response.Employee;

namespace TeqetariApi.Application.Interfaces;

public interface IRegisterEmployeeService
{
    Task<CreateEmployeeResponseDto> RegisterEmployeeAsync(CreateEmployeeDto create, CancellationToken ct);
    Task<CreateEmployeeResponseDto?> GetEmployeeByIdAsync(int id, CancellationToken ct);
    Task<IEnumerable<CreateEmployeeResponseDto>> GetAllEmployeesAsync(CancellationToken ct);
    Task<bool> EmployeeExistsAsync(string nationalId, CancellationToken ct);
}