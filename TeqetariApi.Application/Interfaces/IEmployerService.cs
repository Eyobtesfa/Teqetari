using TeqetariApi.Application.DTOs.Create.Employer;
using TeqetariApi.Application.DTOs.Response.Employer;
using TeqetariApi.Domain.Models;

namespace TeqetariApi.Application.Interfaces;

public interface IEmployerService
{
    // IEmployeeService (or dedicated IEmployerService)
    Task<(bool Success, IEnumerable<EmployerBaseResponseDto> Result, IEnumerable<string> Errors)> GetEmployersAsync(CancellationToken ct);
    /*Task<EmployerBaseResponseDto> RegisterEmployerAsync(CreateEmployerDto create, CancellationToken ct);
    Task<EmployerBaseResponseDto?> GetEmployerByIdAsync(int id, CancellationToken ct);
    Task<List<EmployerBaseResponseDto>> GetAllEmployersAsync(CancellationToken ct);
    Task<Employer?> GetEmployerByPhoneAsync(string phoneNumber, CancellationToken ct);*/
}