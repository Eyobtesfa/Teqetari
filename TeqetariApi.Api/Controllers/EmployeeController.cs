using Microsoft.AspNetCore.Mvc;
using TeqetariApi.Application.DTOs.Create.Employee;
using TeqetariApi.Infrastructure.Services.Employees;
using TeqetariApi.Application.Interfaces;
using TeqetariApi.Application.DTOs.Response.Employee;



namespace TeqetariApi.Api.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeeController(IRegisterEmployeeService registerEmployee) : ControllerBase

{
    [HttpGet("{id:int}", Name = nameof(GetEmployeeById))]
    public async Task<ActionResult> GetEmployeeById(
        int id,
        CancellationToken ct)
    {
        var employee = await registerEmployee.GetEmployeeByIdAsync(id, ct);

        if (employee is null)
        {
            return NotFound(new { message = $"Employee with ID {id} was not found." });
        }

        return Ok(employee);
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CreateEmployeeResponseDto>>> GetAllEmployees(CancellationToken ct)
    {
        var employees = await registerEmployee.GetAllEmployeesAsync(ct);
        return Ok(employees);
    }
    [HttpPost]
    public async Task<IActionResult> RegisterEmployeeAsync([FromBody] CreateEmployeeDto dto, CancellationToken ct)


    {
        if (await registerEmployee.EmployeeExistsAsync(dto.NationalIdNumber, ct))


        {
            return Conflict(new ProblemDetails

            {
                Title = "Employee with the provided National ID Number already exists.",
                Detail = $"An employee with National ID Number {dto.NationalIdNumber} already exists.",
                Status = StatusCodes.Status409Conflict
            });
        }
        var result = await registerEmployee.RegisterEmployeeAsync(dto, ct);
        return CreatedAtAction(
            nameof(GetEmployeeById),
            new { id = result.Id },
            result);
    }
}