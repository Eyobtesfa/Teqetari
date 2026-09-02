using Microsoft.AspNetCore.Mvc;
using TeqetariApi.Application.DTOs.Create.Employee;
using TeqetariApi.Infrastructure.Services.Employees;
using TeqetariApi.Application.Interfaces;
using TeqetariApi.Application.DTOs.Response.Employee;
using Microsoft.AspNetCore.Authorization;
using TeqetariApi.Application.DTOs;



namespace TeqetariApi.Api.Controllers;

[ApiController]
[Route("api/employees")]
[Authorize(Roles = "EMPLOYER")]
public class EmployeeController(IEmployeeService employeeService) : ControllerBase

{


    [HttpGet]
    public async Task<IActionResult> GetEmployees([FromQuery] EmployeeFilterDto filter, CancellationToken ct)
    {
        var (success, result, errors) = await employeeService.GetEmployeesAsync(filter, ct);

        if (!success)
            return BadRequest(new { errors });

        return Ok(result);
    }





    [HttpGet("{id:int}", Name = nameof(GetEmployeeById))]
    public async Task<ActionResult> GetEmployeeById(
        int id,
        CancellationToken ct)
    {
        var employee = await employeeService.GetEmployeeByIdAsync(id, ct);

        if (employee is null)
        {
            return NotFound(new { message = $"Employee with ID {id} was not found." });
        }

        return Ok(employee);
    }
    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<CreateEmployeeResponseDto>>> GetAllEmployees(CancellationToken ct)
    //{
    //   var employees = await employeeService.GetAllEmployeesAsync(ct);
    //  return Ok(employees);
    // }

}