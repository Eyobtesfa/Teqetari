using Microsoft.AspNetCore.Mvc;
using TeqetariApi.Application.DTOs.Create.Employee;
using TeqetariApi.Infrastructure.Services.Employees;
using TeqetariApi.Application.Interfaces;



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
    [HttpPost]
    public async Task<IActionResult> RegisterEmployeeAsync(CreateEmployeeDto create, CancellationToken ct)


    {
        if (await registerEmployee.EmployeeExistsAsync(create.NationalIdNumber, ct))


        {
            return Conflict(new ProblemDetails

            {
                Title = "Employee with the provided National ID Number already exists.",
                Detail = $"An employee with National ID Number {create.NationalIdNumber} already exists.",
                Status = StatusCodes.Status409Conflict
            });
        }
        var result = await registerEmployee.RegisterEmployeeAsync(create, ct);
        return CreatedAtAction(
            nameof(GetEmployeeById),
            new { id = result.Id },
            result);
    }




}