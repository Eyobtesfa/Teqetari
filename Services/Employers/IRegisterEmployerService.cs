using TeqetariApi.DTO.Create.Employer;
using TeqetariApi.DTO.Response.Employer;

namespace TeqetariApi.Services.Employers;

public interface IRegisterEmployerService
{
    Task<EmployerBaseResponseDto> RegisterEmployerAsync(CreateEmployerDto create, CancellationToken ct);
    Task<EmployerBaseResponseDto?> GetEmployerByIdAsync(int id, CancellationToken ct);
}