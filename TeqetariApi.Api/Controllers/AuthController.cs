using Microsoft.AspNetCore.Mvc;
using TeqetariApi.Application.Interfaces;
// Update these using directives to point to where your Create DTOs live
using TeqetariApi.Application.DTOs;
using TeqetariApi.Application.DTOs.Create.Employee;
using TeqetariApi.Application.DTOs.Create.Employer;

namespace TeqetariApi.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController(IAuthService authService) : ControllerBase
{
    /// <summary>
    /// Registers a new Employee using Phone Number and Password.
    /// </summary>
    [HttpPost("register/employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterEmployee([FromBody] CreateEmployeeDto dto)
    {
        var (success, errors) = await authService.RegisterEmployeeAsync(dto);
        if (!success)
        {
            return BadRequest(new { errors });
        }

        return Ok(new { message = "Employee account created successfully." });
    }

    /// <summary>
    /// Registers a new Employer (Household, Private Company, or Government Organization) using Email and Password.
    /// Supports polymorphic payload types.
    /// </summary>
    [HttpPost("register/employer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterEmployer([FromBody] CreateEmployerDto dto)
    {
        var (success, errors) = await authService.RegisterEmployerAsync(dto);
        if (!success)
        {
            return BadRequest(new { errors });
        }

        return Ok(new { message = "Employer account created successfully." });
    }

    /// <summary>
    /// Authenticates Employees (via Phone Number) or Employers (via Email) and issues JWT access and refresh tokens.
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status423Locked)]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var (success, response, errorMessage, statusCode) = await authService.LoginAsync(dto);
        if (!success)
        {
            return statusCode switch
            {
                StatusCodes.Status423Locked => StatusCode(StatusCodes.Status423Locked, new { detail = errorMessage }),
                _ => Unauthorized(new { detail = errorMessage })
            };
        }

        return Ok(response);
    }

    /// <summary>
    /// Rotates a refresh token to obtain a new Access Token and Refresh Token pair.
    /// Implements reuse detection to revoke all active sessions if an already-used token is submitted.
    /// </summary>
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto dto)
    {
        var (success, response, errorMessage) = await authService.RefreshTokenAsync(dto);
        if (!success)
        {
            return Unauthorized(new { detail = errorMessage });
        }

        return Ok(response);
    }
}