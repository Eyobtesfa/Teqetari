using TeqetariApi.Application.DTOs;

using TeqetariApi.Application.DTOs.Response.Employee;
using TeqetariApi.Domain.Models;

namespace TeqetariApi.Application.Interfaces;

public interface IEmployeeService
{
    //Task<CreateEmployeeResponseDto> RegisterEmployeeAsync(CreateEmployeeDto create, CancellationToken ct);
    Task<(bool Success, IEnumerable<EmployeeFilterResponseDto> Result, IEnumerable<string> Errors)> GetEmployeesAsync(EmployeeFilterDto filter, CancellationToken ct);
    Task<CreateEmployeeResponseDto?> GetEmployeeByIdAsync(int id, CancellationToken ct);
    Task<IEnumerable<CreateEmployeeResponseDto>> GetAllEmployeesAsync(CancellationToken ct);
    Task<bool> EmployeeExistsAsync(string nationalId, CancellationToken ct);
    Task<Employee?> GetEmployeeByPhoneAsync(string phoneNumber, CancellationToken ct);
}