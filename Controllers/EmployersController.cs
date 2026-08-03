using Microsoft.AspNetCore.Mvc;
using TeqetariApi.DTO.Create.Employer;

using TeqetariApi.Services.Employers;

namespace TeqetariApi.Controllers;

[ApiController]
[Route("api/employers")]
public class EmployersController(IRegisterEmployerService registerEmployee, ILogger<EmployersController> logger) : ControllerBase
{

    [HttpGet("{id:int}", Name = nameof(GetEmployerById))]
    public async Task<ActionResult> GetEmployerById(
        int id,
        CancellationToken ct)
    {
        var employer = await registerEmployee.GetEmployerByIdAsync(id, ct);

        if (employer is null)
        {
            return NotFound(new { message = $"Employer with ID {id} was not found." });
        }

        return Ok(employer);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterEmployer(CreateEmployerDto create, CancellationToken ct)
    {
        logger.LogInformation("Received registration request for email: {Email}", create.Email);

        try
        {
            var result = await registerEmployee.RegisterEmployerAsync(create, ct);

            // Returns HTTP 201 Created with a Location header pointing to GetById
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
}