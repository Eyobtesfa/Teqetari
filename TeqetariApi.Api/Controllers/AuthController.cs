using Microsoft.AspNetCore.Mvc;
using TeqetariApi.Application.DTOs.Response.Employer;
using TeqetariApi.Application.Interfaces;

namespace TeqetariApi.Api.Controllers;

public record LoginRequestDto(string PhoneNumber, string Password);
public record UserProfileDto(string DisplayName, string Role, int Id);

[ApiController]
[Route("api/auth")]
public class AuthController(
    IRegisterEmployeeService employeeService,
    IRegisterEmployerService employerService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequestDto request,
        [FromServices] IWebHostEnvironment env,
        CancellationToken ct)
    {
        var employee = await employeeService.GetEmployeeByPhoneAsync(request.PhoneNumber, ct);
        if (employee is not null && BCrypt.Net.BCrypt.Verify(request.Password, employee.PasswordHash))
        {
            IssueAuthCookies(employee.Id, "Employee", env);
            return Ok(new UserProfileDto($"{employee.FirstName} {employee.LastName}", "Employee", employee.Id));
        }

        var employer = await employerService.GetEmployerByPhoneAsync(request.PhoneNumber, ct);
        if (employer is not null && BCrypt.Net.BCrypt.Verify(request.Password, employer.PasswordHash))
        {
            IssueAuthCookies(employer.Id, "Employer", env);
            return Ok(new UserProfileDto(employer.DisplayName, "Employer", employer.Id));
        }

        return Unauthorized(new { detail = "Invalid phone number or password." });
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser(CancellationToken ct)
    {
        if (!Request.Cookies.TryGetValue("teqetari_id", out var idValue) ||
            !Request.Cookies.TryGetValue("teqetari_role", out var role) ||
            !int.TryParse(idValue, out var id))
        {
            return Unauthorized(new { detail = "Session expired or missing." });
        }

        if (role == "Employee")
        {
            var employee = await employeeService.GetEmployeeByIdAsync(id, ct);
            return employee is null
                ? Unauthorized()
                : Ok(new UserProfileDto($"{employee.FirstName} {employee.LastName}", "Employee", employee.Id));
        }
        else
        {
            var employer = await employerService.GetEmployerByIdAsync(id, ct);
            if (employer is null) return Unauthorized();

            var displayName = employer switch
            {
                HouseholdResponseDto h => h.FullName,
                PrivateCompanyResponseDto c => c.CompanyName,
                GovernmentOrganizationResponseDto g => g.OrganizationName,
                _ => "Employer"
            };

            return Ok(new UserProfileDto(displayName, "Employer", employer.Id));
        }
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("teqetari_id");
        Response.Cookies.Delete("teqetari_role");
        return Ok();
    }

    private void IssueAuthCookies(int userId, string role, IWebHostEnvironment env)
    {
        var options = new CookieOptions
        {
            HttpOnly = true,
            Secure = !env.IsDevelopment(),
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddHours(8)
        };
        Response.Cookies.Append("teqetari_id", userId.ToString(), options);
        Response.Cookies.Append("teqetari_role", role, options);
    }
}