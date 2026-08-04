using TeqetariApi.DTO.Create.JobPost;
using TeqetariApi.DTO.Response.JobPost;

public interface IApplyToJobService
{
    Task<JobPostResponseDto> ApplyToJobAsync(CreateJobPostDto create, CancellationToken ct);
}