using Microsoft.AspNetCore.Mvc;
using TeqetariApi.Application.DTOs.Create.Employer;

using TeqetariApi.Infrastructure.Services.Employers;
using TeqetariApi.Application.Interfaces;
using TeqetariApi.Application.DTOs.Response.Employer;
using Microsoft.AspNetCore.Authorization;

namespace TeqetariApi.Api.Controllers;

[ApiController]
[Route("api/employers")]
[Authorize(Roles = "EMPLOYEE")]
public class EmployersController(IEmployerService employerService, ILogger<EmployersController> logger) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetEmployers(CancellationToken ct)
    {
        var (success, result, errors) = await employerService.GetEmployersAsync(ct);

        logger.LogInformation("");
        return success ? Ok(result) : BadRequest(new { errors });
    }
}
