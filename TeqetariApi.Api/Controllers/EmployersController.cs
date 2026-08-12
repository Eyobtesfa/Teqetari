using Microsoft.AspNetCore.Mvc;
using TeqetariApi.Application.DTOs.Create.Employer;

using TeqetariApi.Infrastructure.Services.Employers;
using TeqetariApi.Application.Interfaces;
using TeqetariApi.Application.DTOs.Response.Employer;

namespace TeqetariApi.Api.Controllers;

[ApiController]
[Route("api/employers")]
public class EmployersController(IRegisterEmployerService registerEmployer, ILogger<EmployersController> logger) : ControllerBase
{

    [HttpGet("{id:int}", Name = nameof(GetEmployerById))]
    public async Task<ActionResult> GetEmployerById(
        int id,
        CancellationToken ct)
    {
        var employer = await registerEmployer.GetEmployerByIdAsync(id, ct);

        if (employer is null)
        {
            return NotFound(new { message = $"Employer with ID {id} was not found." });
        }

        return Ok(employer);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterEmployer([FromBody]CreateEmployerDto dto, CancellationToken ct)
    {
        logger.LogInformation("Received registration request for email: {Email}", dto.Email);

        try
        {
            var result = await registerEmployer.RegisterEmployerAsync(dto, ct);

          
            return CreatedAtAction(
                nameof(GetEmployerById),
                new { id = result.Id },
                result);
        }
        catch (InvalidOperationException ex)
        {
            logger.LogWarning("Registration failed due to conflict: {Message}", ex.Message);
            return Conflict(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning("Registration failed due to invalid arguments: {Message}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpGet]
    public async Task<ActionResult<List<EmployerBaseResponseDto>>> GetAllEmployers(CancellationToken ct)
    {
        var employers = await registerEmployer.GetAllEmployersAsync(ct);
        return Ok(employers);
    }
}