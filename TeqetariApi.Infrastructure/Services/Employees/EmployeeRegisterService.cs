using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TeqetariApi.Infrastructure.Persistence;
using TeqetariApi.Application.DTOs.Create.Employee;
using TeqetariApi.Application.DTOs.Response.Employee;
using TeqetariApi.Domain.Models;
using TeqetariApi.Application.Interfaces;

namespace TeqetariApi.Infrastructure.Services.Employees;

public class EmployeeRegisterService(TeqetariDbContext context, ILogger<EmployeeRegisterService> logger) : IRegisterEmployeeService
{

    public async Task<IEnumerable<CreateEmployeeResponseDto>> GetAllEmployeesAsync(CancellationToken ct)
    {
        var employees = await context.Employees
                .AsNoTracking()
                .Select(e => new CreateEmployeeResponseDto
                {
                    Id = e.Id,
                    PhoneNumber = e.PhoneNumber,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    NationalIdNumber = e.NationalIdNumber,
                    City = e.City,
                    SubCity = e.SubCity,
                    Woreda = e.Woreda,
                    YearsOfExperience = e.YearsOfExperience,
                    ExpectedSalary = e.ExpectedSalary,
                    JobCategory = e.JobCategory,
                    Skills = e.Skills,
                    IsAvailable = e.IsAvailable,
                    BackgroundCheckPassed = e.BackgroundCheckPassed,
                    RegisteredAt = e.RegisteredAt,
                    TotalApplicationCount = e.TotalApplicationCount
                })
                .ToListAsync(ct);

                return employees;
    }
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

    public Task<bool> EmployeeExistsAsync(string nationalId, CancellationToken ct) =>
        context.Employees.AsNoTracking().AnyAsync(e => e.NationalIdNumber == nationalId, ct);


}
