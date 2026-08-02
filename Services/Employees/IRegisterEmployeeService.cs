using TeqetariApi.DTO.Create.Employee;
using TeqetariApi.DTO.Response.Employee;

namespace TeqetariApi.Services.Employees;

public interface IRegisterEmployeeService
{
    Task<CreateEmployeeResponseDto> RegisterEmployeeAsync(CreateEmployeeDto create, CancellationToken ct);
}