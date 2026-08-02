using TeqetariApi.Data;
using TeqetariApi.DTO.Create.Employee;
using TeqetariApi.DTO.Response.Employee;
using TeqetariApi.Models;

namespace TeqetariApi.Services.Employees;

public class EmployeeRegisterService(TeqetariDbContext context, ILogger<EmployeeRegisterService> logger) : IRegisterEmployeeService
{
    public async Task<CreateEmployeeResponseDto> RegisterEmployeeAsync(CreateEmployeeDto create, CancellationToken ct)
    {
        logger.LogInformation("Attempting to register employee with Email: {Email}", create.Email);

        var employee = new Employee
        {
            PhoneNumber = create.PhoneNumber,
            FirstName = create.FirstName,
            LastName = create.LastName,
            NationalIdNumber = create.NationalIdNumber,
            City = create.City,
            SubCity = create.SubCity,
            Woreda = create.Woreda,
            YearsOfExperience = create.YearsOfExperience,
            Email = create.Email,
            Skills = create.Skills,
        };

        context.Employees.Add(employee);
        await context.SaveChangesAsync(ct);
        logger.LogInformation("Successfully registered employee with ID: {EmployeeId}", employee.Id);
        return (await GetByIdAsync(employee.Id, ct))!;
    }
}
