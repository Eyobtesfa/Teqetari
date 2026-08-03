using Microsoft.EntityFrameworkCore;
using TeqetariApi.Data;
using TeqetariApi.DTO.Create.Employee;
using TeqetariApi.DTO.Response.Employee;
using TeqetariApi.Models;

namespace TeqetariApi.Services.Employees;

public class EmployeeRegisterService(TeqetariDbContext context, ILogger<EmployeeRegisterService> logger) : IRegisterEmployeeService
{
    public async Task<CreateEmployeeResponseDto?> GetEmployeeByIdAsync(int id, CancellationToken ct)
    {
        logger.LogInformation("Fetching employee with ID: {EmployeeId}", id);
        var employee = await context.Employees.FindAsync(new object[] { id }, ct);
        if (employee == null)
        {
            logger.LogWarning("Employee with ID: {EmployeeId} not found", id);
            return null;
        }

        return new CreateEmployeeResponseDto
        {
            Id = employee.Id,
            PhoneNumber = employee.PhoneNumber,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            NationalIdNumber = employee.NationalIdNumber,
            City = employee.City,
            SubCity = employee.SubCity,
            Woreda = employee.Woreda,
            YearsOfExperience = employee.YearsOfExperience,
            Skills = employee.Skills ?? new List<string>(),
            ExpectedSalary = employee.ExpectedSalary,
            JobCategory = employee.JobCategory,
            IsAvailable = employee.IsAvailable,
            BackgroundCheckPassed = employee.BackgroundCheckPassed,
            RegisteredAt = employee.RegisteredAt,
            TotalApplicationCount = employee.TotalApplicationCount
        };
    }
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
            ExpectedSalary = create.ExpectedSalary,
            JobCategory = create.JobCategory,
           
        };

        context.Employees.Add(employee);
        await context.SaveChangesAsync(ct);
        logger.LogInformation("Successfully registered employee with ID: {EmployeeId}", employee.Id);
        return (await GetEmployeeByIdAsync(employee.Id, ct))!;
    }

    public  Task<bool> EmployeeExistsAsync(string nationalId, CancellationToken ct) =>
        context.Employees.AsNoTracking().AnyAsync(e=> e.NationalIdNumber == nationalId, ct);
    
        
}
