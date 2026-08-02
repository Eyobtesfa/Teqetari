using Microsoft.EntityFrameworkCore;
using TeqetariApi.Models;

namespace TeqetariApi.Data;

public class TeqetariDbContext(DbContextOptions<TeqetariDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Employer> Employers => Set<Employer>();
    public DbSet<Household> Households => Set<Household>();
    public DbSet<PrivateCompany> PrivateCompanies => Set<PrivateCompany>();
    public DbSet<GovernmentOrganization> GovernmentOrganizations => Set<GovernmentOrganization>();
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();
    public DbSet<JobPost> JobPosts => Set<JobPost>();
    public DbSet<PlacementContract> PlacementContracts => Set<PlacementContract>();
}