using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeqetariApi.Application.DTOs.Create;
using TeqetariApi.Application.DTOs.Update;
using TeqetariApi.Application.Interfaces;

namespace TeqetariApi.Api.Controllers;

[ApiController]
[Route("api/hire-request")]
[Authorize]
public class HireRequestController(IHireRequestService hireRequest) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "EMPLOYER")]
    public async Task<IActionResult> Send(HireRequestDto dto, CancellationToken ct)
    {
        var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var (success, result, errors) = await hireRequest.SendHireRequestAsync(appUserId!, dto, ct);

        if (!success)
            return BadRequest(new { errors });

        return Ok(result);
    }
    [HttpGet("sent")]
    [Authorize(Roles = "EMPLOYER")]
    public async Task<IActionResult> GetSent(CancellationToken ct)
    {
        var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var (success, result, errors) = await hireRequest.GetSentHireRequestsAsync(appUserId!, ct);
        return success ? Ok(result) : BadRequest(new { errors });
    }
    [HttpGet("received")]
    [Authorize(Roles = "EMPLOYEE")]
    public async Task<IActionResult> GetReceived(CancellationToken ct)
    {
        var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var (success, result, errors) = await hireRequest.GetReceivedHireRequestsAsync(appUserId!, ct);
        return success ? Ok(result) : BadRequest(new { errors });
    }
    [HttpPatch("{id:int}/respond")]
    [Authorize(Roles = "EMPLOYEE")]
    public async Task<IActionResult> Respond(int id, HireRequestUpdateDto dto, CancellationToken ct)
    {
        var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var (success, error) = await hireRequest.RespondToHireRequestAsync(appUserId!, id, dto, ct);
        return success ? Ok(new { message = dto.Accept ? "Hire request accepted." : "Hire request declined." }) : BadRequest(new { error });
    }
}