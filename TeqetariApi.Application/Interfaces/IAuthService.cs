using TeqetariApi.Application.DTOs.Create.Employee;
using TeqetariApi.Application.DTOs.Create.Employer;
using TeqetariApi.Domain.Enums;
using TeqetariApi.Domain.Models;

namespace TeqetariApi.Application.Interfaces;



public record LoginDto(string Identifier, string Password);
public record RefreshTokenDto(string RefreshToken);
public record AuthResponseDto(string AccessToken, string RefreshToken);

public interface IAuthService
{
    Task<(bool Success, IEnumerable<string> Errors)> RegisterEmployeeAsync(CreateEmployeeDto dto);
    Task<(bool Success, IEnumerable<string> Errors)> RegisterEmployerAsync(CreateEmployerDto dto);
    Task<(bool Success, AuthResponseDto? Response, string? ErrorMessage, int? StatusCode)> LoginAsync(LoginDto dto);
    Task<(bool Success, AuthResponseDto? Response, string? ErrorMessage)> RefreshTokenAsync(RefreshTokenDto dto);
}