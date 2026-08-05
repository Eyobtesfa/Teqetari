using TeqetariApi.DTO.Create.JobPost;
using TeqetariApi.DTO.Response.JobPost;
using TeqetariApi.Enums;

namespace TeqetariApi.Services.JobPosts;
public interface IPostJobService
{
    Task<JobPostResponseDto> PostJobAsync(CreateJobPostDto create, CancellationToken ct);
    Task<IEnumerable<JobPostResponseDto?>> GetJobPostByCategoryAsync(JobCategory category, CancellationToken ct);
}