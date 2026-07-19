using Microsoft.EntityFrameworkCore;
using TeqetariApi.Models;

namespace TeqetariApi.Data;

public class TeqetariDbContext(DbContextOptions<TeqetariDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Employer> Employers => Set<Employer>();
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();
    public DbSet<JobPost> JobPosts => Set<JobPost>();
    public DbSet<PlacementContract> PlacementContracts => Set<PlacementContract>();
}