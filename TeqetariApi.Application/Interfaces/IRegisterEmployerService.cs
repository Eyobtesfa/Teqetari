using TeqetariApi.Application.DTOs.Create.Employer;
using TeqetariApi.Application.DTOs.Response.Employer;
using TeqetariApi.Domain.Models;

namespace TeqetariApi.Application.Interfaces;

public interface IRegisterEmployerService
{
    Task<EmployerBaseResponseDto> RegisterEmployerAsync(CreateEmployerDto create, CancellationToken ct);
    Task<EmployerBaseResponseDto?> GetEmployerByIdAsync(int id, CancellationToken ct);
    Task<List<EmployerBaseResponseDto>> GetAllEmployersAsync(CancellationToken ct);
    Task<Employer?> GetEmployerByPhoneAsync(string phoneNumber, CancellationToken ct);
}