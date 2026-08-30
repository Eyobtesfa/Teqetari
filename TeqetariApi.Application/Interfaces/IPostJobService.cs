using TeqetariApi.Application.DTOs.Create.JobPost;
using TeqetariApi.Application.DTOs.Response.JobPost;
using TeqetariApi.Domain.Enums;

namespace TeqetariApi.Application.Interfaces;

public interface IPostJobService
{
    Task<(bool Success, JobPostResponseDto? result, IEnumerable<string> Errors)> PostJobAsync(string appUserId, CreateJobPostDto create, CancellationToken ct);
    Task<(bool Success, IEnumerable<JobPostResponseDto> Result, IEnumerable<string> Errors)> GetMyJobsAsync(string appUserId, CancellationToken ct);
}