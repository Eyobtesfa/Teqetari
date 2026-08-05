using Microsoft.EntityFrameworkCore;
using TeqetariApi.Data;
using TeqetariApi.Models;
using TeqetariApi.DTO.Create.JobPost;
using TeqetariApi.DTO.Response.JobPost;
using TeqetariApi.Enums;


namespace TeqetariApi.Services.JobPosts;
public class PostJobService(TeqetariDbContext context, ILogger<PostJobService> logger) : IPostJobService

{
    public async Task<IEnumerable<JobPostResponseDto?>> GetJobPostByCategoryAsync(JobCategory category,
        CancellationToken ct)
    {
        logger.LogInformation("Fetching job posts for category: {Category}", category);
        var jobPosts = await context.JobPosts
            .Where(jp => jp.Category == category)
            .ToListAsync(ct);

        if (!jobPosts.Any())
        {
            logger.LogWarning("No job posts found for category: {Category}", category);
            return Enumerable.Empty<JobPostResponseDto>();
        }

        return jobPosts.Select(jp => new JobPostResponseDto
        {
            Id = jp.Id,
            EmployerId = jp.EmployerId,
            Title = jp.Title,
            Description = jp.Description,
            Category = jp.Category,
            RequiredSkills = jp.RequiredSkills,
            OfferedSalaryMin = jp.OfferedSalaryMin,
            OfferedSalaryMax = jp.OfferedSalaryMax,
            Location = jp.Location,
            AccommodationProvided = jp.AccommodationProvided,
            MinimumExperienceYears = jp.MinimumExperienceYears,
            ExpirationDate = jp.ExpirationDate,
            PostedAt = jp.PostedAt
        });
    }
    public async Task<JobPostResponseDto> PostJobAsync(CreateJobPostDto create, CancellationToken ct)


    {
        logger.LogInformation("Attempting to post job with Title: {Title}", create.Title);

        var jobPost = new JobPost


        {
           Title = create.Title,
           Description = create.Description,
           Category = create.Category,
           OfferedSalaryMin = create.OfferedSalaryMin,
           OfferedSalaryMax = create.OfferedSalaryMax,
           RequiredSkills = create.RequiredSkills,
           Location = create.Location,
           AccommodationProvided = create.AccommodationProvided,
           MinimumExperienceYears = create.MinimumExperienceYears,
           ExpirationDate = create.ExpirationDate,
           PostedAt = DateTime.UtcNow,
        };

        context.JobPosts.Add(jobPost);
        await context.SaveChangesAsync(ct);
        logger.LogInformation("Job posted successfully with ID: {JobPostId}", jobPost.Id);
        return new JobPostResponseDto
        {
            Id = jobPost.Id,
            EmployerId = jobPost.EmployerId,
            Title = jobPost.Title,
            Description = jobPost.Description,
            Category = jobPost.Category,
            OfferedSalaryMin = jobPost.OfferedSalaryMin,
            OfferedSalaryMax = jobPost.OfferedSalaryMax,
            RequiredSkills = jobPost.RequiredSkills,
            Location = jobPost.Location,
            AccommodationProvided = jobPost.AccommodationProvided,
            MinimumExperienceYears = jobPost.MinimumExperienceYears,
            ExpirationDate = jobPost.ExpirationDate,
            PostedAt = jobPost.PostedAt
        };
    }
    
}