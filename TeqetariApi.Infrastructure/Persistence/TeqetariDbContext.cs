using Microsoft.EntityFrameworkCore;
using TeqetariApi.Domain.Models;
using TeqetariApi.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TeqetariApi.Domain.Enums;

namespace TeqetariApi.Infrastructure.Persistence;

public class TeqetariDbContext : IdentityDbContext<AppUser>
{
    public TeqetariDbContext(DbContextOptions<TeqetariDbContext> options) : base(options) { }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Employer> Employers => Set<Employer>();
    public DbSet<Household> Households => Set<Household>();
    public DbSet<PrivateCompany> PrivateCompanies => Set<PrivateCompany>();
    public DbSet<GovernmentOrganization> GovernmentOrganizations => Set<GovernmentOrganization>();
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();
    public DbSet<JobPost> JobPosts => Set<JobPost>();
    public DbSet<PlacementContract> PlacementContracts => Set<PlacementContract>();
    public DbSet<HireRequest> HireRequests => Set<HireRequest>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Employer>()
            .HasDiscriminator<EmployerType>("Type")
            .HasValue<Household>(EmployerType.Household)
            .HasValue<PrivateCompany>(EmployerType.PrivateCompany)
            .HasValue<GovernmentOrganization>(EmployerType.GovernmentOrganization);

        builder.Entity<Employee>()
            .HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(e => e.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Employer>()
            .HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(e => e.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}