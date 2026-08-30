using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TeqetariApi.Application.DTOs.Create.JobPost;
using TeqetariApi.Application.DTOs.Response.JobPost;
using TeqetariApi.Application.Interfaces;
using TeqetariApi.Domain.Models;
using TeqetariApi.Infrastructure.Persistence;

namespace TeqetariApi.Infrastructure.Services.JobPosts;

public class JobPostService(TeqetariDbContext context, ILogger<JobPostService> logger) : IPostJobService
{
    public async Task<(bool Success, JobPostResponseDto? result, IEnumerable<string> Errors)> PostJobAsync(string appUserId, CreateJobPostDto dto, CancellationToken ct)
    {
        var employer = await context.Employers.FirstOrDefaultAsync(e => e.AppUserId == appUserId, ct);
        if (employer is null)
        {
            logger.LogWarning(
               "Job post rejected: no Employer profile found for AppUserId {AppUserId}.",
               appUserId);
            return (false, null, new[] { "Employer profile not found" });
        }

        var jobPost = new JobPost
        {
            EmployerId = employer.Id,
            Title = dto.Title,
            Description = dto.Description,
            Category = dto.Category,
            RequiredSkills = dto.RequiredSkills,
            OfferedSalaryMin = dto.OfferedSalaryMin,
            OfferedSalaryMax = dto.OfferedSalaryMax,
            WorkingMode = dto.WorkMode,
            MinimumExperienceYears = dto.MinimumExperienceYears,
            Location = dto.Location,
            PostedAt = DateTime.UtcNow,
            ExpirationDate = DateTime.SpecifyKind(dto.ExpirationDate, DateTimeKind.Utc)
        };

        context.JobPosts.Add(jobPost);

        await context.SaveChangesAsync(ct);

        logger.LogInformation("Employer {EmployerId} posted job {JobTitle} (JobPostId: {JobPostId}).",
        employer.Id, jobPost.Title, jobPost.Id);

        var response = new JobPostResponseDto
        {
            Id = jobPost.Id,
            EmployerId = employer.Id,
            Title = jobPost.Title,
            Description = jobPost.Description,
            Category = jobPost.Category,
            RequiredSkills = jobPost.RequiredSkills,
            OfferedSalaryMin = jobPost.OfferedSalaryMin,
            OfferedSalaryMax = jobPost.OfferedSalaryMax,
            MinimumExperienceYears = jobPost.MinimumExperienceYears,
            Location = jobPost.Location,
            WorkMode = jobPost.WorkingMode,
            PostedAt = DateTime.Now,
            ExpirationDate = jobPost.ExpirationDate
        };

        return (true, response, Enumerable.Empty<string>());

    }

    public async Task<(bool Success, IEnumerable<JobPostResponseDto> Result, IEnumerable<string> Errors)> GetMyJobsAsync(
    string appUserId, CancellationToken ct)
    {
        var employer = await context.Employers
            .FirstOrDefaultAsync(e => e.AppUserId == appUserId, ct);

        if (employer is null)
        {
            logger.LogWarning("GetMyJobs rejected: no Employer profile for AppUserId {AppUserId}.", appUserId);
            return (false, Enumerable.Empty<JobPostResponseDto>(), new[] { "Employer profile not found." });
        }

        var jobs = await context.JobPosts
            .Where(j => j.EmployerId == employer.Id)
            .OrderByDescending(j => j.PostedAt)
            .Select(j => new JobPostResponseDto
            {
                Id = j.Id,
                EmployerId = j.EmployerId,
                Title = j.Title,
                Description = j.Description,
                Category = j.Category,
                OfferedSalaryMin = j.OfferedSalaryMin,
                OfferedSalaryMax = j.OfferedSalaryMax,
                RequiredSkills = j.RequiredSkills,
                Location = j.Location,
                WorkMode = j.WorkingMode,
                MinimumExperienceYears = j.MinimumExperienceYears,
                ExpirationDate = j.ExpirationDate,
                PostedAt = j.PostedAt,

            })
            .ToListAsync(ct);

        logger.LogInformation("Employer {EmployerId} fetched {Count} job posting(s).", employer.Id, jobs.Count);

        return (true, jobs, Enumerable.Empty<string>());
    }
}