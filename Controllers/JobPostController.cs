using Microsoft.AspNetCore.Mvc;
using TeqetariApi.Services.JobPosts;
using TeqetariApi.DTO.Create.JobPost;
using TeqetariApi.Enums;


namespace TeqetariApi.Controllers;

[ApiController]
[Route("api/jobposts")]
public class JobPostController(IPostJobService postJob) : ControllerBase


{
    [HttpGet("category/{category}", Name = nameof(GetJobPostByCategory))]

    public async Task<IActionResult> GetJobPostByCategory(
        JobCategory category,
        CancellationToken ct)
    {
        var jobPosts = await postJob.GetJobPostByCategoryAsync(category, ct);
        return Ok(jobPosts);
    }
    [HttpPost]
    public async Task<IActionResult> PostJobAsync(CreateJobPostDto create, CancellationToken ct)


    {
        var result =  await postJob.PostJobAsync(create, ct);
        return CreatedAtAction(
            nameof(GetJobPostByCategory),
            new { category = result.Category },
            result);
    }
}