using TeqetariApi.DTO.Create.Employer;
using TeqetariApi.DTO.Response.Employer;

namespace TeqetariApi.Services;

public interface IRegisterEmployeeService
{
    Task<EmployerBaseResponseDto> RegisterEmployerAsync(CreateEmployerDto create, CancellationToken ct);
    Task<EmployerBaseResponseDto?> GetEmployerByIdAsync(int id, CancellationToken ct);
}