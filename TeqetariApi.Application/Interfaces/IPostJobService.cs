using TeqetariApi.Application.DTOs.Create.JobPost;
using TeqetariApi.Application.DTOs.Response.JobPost;
using TeqetariApi.Domain.Enums;

namespace TeqetariApi.Application.Interfaces;

public interface IPostJobService
{
    Task<JobPostResponseDto> PostJobAsync(CreateJobPostDto create, CancellationToken ct);
    Task<IEnumerable<JobPostResponseDto?>> GetJobPostByCategoryAsync(JobCategory category, CancellationToken ct);
}