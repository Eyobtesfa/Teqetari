using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeqetariApi.Application.DTOs.Create.Employee;
using TeqetariApi.Application.DTOs.Create.Employer;
using TeqetariApi.Application.Interfaces;

using TeqetariApi.Domain.Models;
using TeqetariApi.Infrastructure.Identity;
using TeqetariApi.Infrastructure.Persistence;

namespace TeqetariApi.Infrastructure.Services;

public class AuthService(
    UserManager<AppUser> userManager,
    RoleManager<IdentityRole> roleManager,
    TeqetariDbContext context,
    TokenService tokenService) : IAuthService
{
    public async Task<(bool Success, IEnumerable<string> Errors)> RegisterEmployeeAsync(CreateEmployeeDto dto)
    {
        // 1. Create Identity User (Employees log in using Phone Number)
        var user = new AppUser
        {
            UserName = dto.PhoneNumber,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            UserType = "Employee"
        };

        var result = await userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            return (false, result.Errors.Select(e => e.Description));
        }

        await userManager.AddToRoleAsync(user, "Employee");

        // 2. Create Employee Domain Entity
        var employee = new Employee
        {
            AppUserId = user.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            NationalIdNumber = dto.NationalIdNumber,
            City = dto.City,
            SubCity = dto.SubCity,
            Woreda = dto.Woreda,
            YearsOfExperience = dto.YearsOfExperience,
            ExpectedSalary = dto.ExpectedSalary,
            JobCategory = dto.JobCategory,
            Skills = dto.Skills ?? []
        };

        context.Employees.Add(employee);
        await context.SaveChangesAsync();

        return (true, Enumerable.Empty<string>());
    }

    public async Task<(bool Success, IEnumerable<string> Errors)> RegisterEmployerAsync(CreateEmployerDto dto)
    {
        // 1. Create Identity User (Employers log in using Email)
        var user = new AppUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            UserType = "Employer"
        };

        var result = await userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            return (false, result.Errors.Select(e => e.Description));
        }

        await userManager.AddToRoleAsync(user, "Employer");

        // 2. Instantiate TPH Employer Subtype via Pattern Matching
        Employer employer = dto switch
        {
            CreateHouseholdEmployerDto h => new Household
            {
                AppUserId = user.Id,
                Type = h.EmployerType,
                Email = h.Email,
                PhoneNumber = h.PhoneNumber,
                City = h.City,
                SubCity = h.SubCity,
                Woreda = h.Woreda,
                SpecialInstruction = h.SpecialInstruction,
                FirstName = h.FirstName,
                LastName = h.LastName,
                NationalIdNumber = h.NationalIdNumber,
                NumberOfFamilyMembers = h.NumberOfFamilyMembers,
                HasPets = h.HasPets ?? false
            },
            CreateCompanyEmployerDto c => new PrivateCompany
            {
                AppUserId = user.Id,
                Type = c.EmployerType,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                City = c.City,

                SubCity = c.SubCity,
                Woreda = c.Woreda,
                SpecialInstruction = c.SpecialInstruction,
                CompanyName = c.CompanyName,
                Industry = c.Industry,
                TradeLicenseNumber = c.TradeLicenseNumber,
                TaxRegistrationNumber = c.TaxRegistrationNumber,
                ContactPersonName = c.ContactPersonName,
                ContactPersonRole = c.ContactPersonRole,
                Size = c.CompanySize
            },
            CreateGovernmentEmployerDto g => new GovernmentOrganization
            {
                AppUserId = user.Id,
                Type = g.EmployerType,
                Email = g.Email,
                PhoneNumber = g.PhoneNumber,
                City = g.City,
                SubCity = g.SubCity,
                Woreda = g.Woreda,
                SpecialInstruction = g.SpecialInstruction,
                OrganizationName = g.OrganizationName,
                Sector = g.Sector,
                Department = g.Department,
                AuthorizedOfficerName = g.AuthorizedOfficerName,
                OfficialLetterRefNumber = g.OfficialLetterRefNumber
            },
            _ => throw new ArgumentOutOfRangeException(nameof(dto), "Unsupported employer type.")
        };

        context.Employers.Add(employer);
        await context.SaveChangesAsync();

        return (true, Enumerable.Empty<string>());
    }

    public async Task<(bool Success, AuthResponseDto? Response, string? ErrorMessage, int? StatusCode)> LoginAsync(LoginDto dto)
    {
        // 1. Search user by Email (Employer) or Phone Number (Employee)
        var user = await userManager.FindByEmailAsync(dto.Identifier)
                ?? await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == dto.Identifier);

        if (user == null)
            return (false, null, "Invalid credentials.", 401);

        // 2. Lockout Check[cite: 1]
        if (await userManager.IsLockedOutAsync(user))
            return (false, null, "Account locked due to multiple failed attempts. Try again later.", 423);

        // 3. Password Verification[cite: 1]
        if (!await userManager.CheckPasswordAsync(user, dto.Password))
        {
            await userManager.AccessFailedAsync(user);
            return (false, null, "Invalid credentials.", 401);
        }

        await userManager.ResetAccessFailedCountAsync(user);

        // Update login timestamp
        user.LastLoginAt = DateTime.UtcNow;
        await userManager.UpdateAsync(user);

        // 4. Issue Tokens[cite: 2]
        var roles = await userManager.GetRolesAsync(user);
        var accessToken = tokenService.GenerateJwt(user, roles);

        var refreshToken = new RefreshToken
        {
            Token = Guid.NewGuid().ToString("N"),
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsUsed = false,
            IsRevoked = false
        };

        context.RefreshTokens.Add(refreshToken);
        await context.SaveChangesAsync();

        return (true, new AuthResponseDto(accessToken, refreshToken.Token), null, null);
    }

    public async Task<(bool Success, AuthResponseDto? Response, string? ErrorMessage)> RefreshTokenAsync(RefreshTokenDto dto)
    {
        var storedToken = await context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == dto.RefreshToken);
        if (storedToken == null)
            return (false, null, "Invalid refresh token.");

        // Theft Detection: If an ALREADY-USED token is submitted, revoke ALL tokens for this account[cite: 2]
        if (storedToken.IsUsed)
        {
            var userTokens = await context.RefreshTokens.Where(rt => rt.UserId == storedToken.UserId).ToListAsync();
            foreach (var token in userTokens)
            {
                token.IsRevoked = true;
            }
            await context.SaveChangesAsync();
            return (false, null, "Security alert: Revoked token reused. All sessions ended.");
        }

        if (storedToken.IsRevoked || storedToken.ExpiresAt < DateTime.UtcNow)
            return (false, null, "Refresh token expired or revoked.");

        storedToken.IsUsed = true;

        var newRefreshToken = new RefreshToken
        {
            Token = Guid.NewGuid().ToString("N"),
            UserId = storedToken.UserId,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsUsed = false,
            IsRevoked = false
        };

        context.RefreshTokens.Add(newRefreshToken);
        await context.SaveChangesAsync();

        var user = await userManager.FindByIdAsync(storedToken.UserId);
        var roles = await userManager.GetRolesAsync(user!);
        var newAccessToken = tokenService.GenerateJwt(user!, roles);

        return (true, new AuthResponseDto(newAccessToken, newRefreshToken.Token), null);
    }

    private async Task EnsureRoleExistsAsync(string roleName)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}