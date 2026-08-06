using TeqetariApi.Application.DTOs.Create.Employer;
using TeqetariApi.Application.DTOs.Response.Employer;

namespace TeqetariApi.Application.Interfaces;

public interface IRegisterEmployerService
{
    Task<EmployerBaseResponseDto> RegisterEmployerAsync(CreateEmployerDto create, CancellationToken ct);
    Task<EmployerBaseResponseDto?> GetEmployerByIdAsync(int id, CancellationToken ct);
}