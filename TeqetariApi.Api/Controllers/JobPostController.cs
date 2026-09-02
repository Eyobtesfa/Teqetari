using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeqetariApi.Application.DTOs.Create.JobPost;
using TeqetariApi.Application.Interfaces;

namespace TeqetariApi.Api.Controllers;

[ApiController]
[Route("api/postJob")]
[Authorize(Roles = "EMPLOYER")]
public class JobPostController(IPostJobService postJob) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostJob([FromBody] CreateJobPostDto dto, CancellationToken ct)
    {
        var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var (success, result, errors) = await postJob.PostJobAsync(appUserId!, dto, ct);
        if (!success)
        {
            return BadRequest(new { errors });
        }
        return Ok(result);
    }
    [HttpGet("mine")]
    public async Task<IActionResult> GetMyJobs(CancellationToken ct)
    {
        var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var (success, result, errors) = await postJob.GetMyJobsAsync(appUserId!, ct);

        if (!success)
            return BadRequest(new { errors });

        return Ok(result);
    }
}