using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeqetariApi.Application.DTOs.Create;
using TeqetariApi.Application.Interfaces;

namespace TeqetariApi.Api.Controllers;

[ApiController]
[Route("api/hire-requests")]
[Authorize]
public class HireRequestController(IHireRequestService hireRequest) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "Employer")]
    public async Task<IActionResult> Send(HireRequestDto dto, CancellationToken ct)
    {
        var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var (success, result, errors) = await hireRequest.SendHireRequestAsync(appUserId!, dto, ct);

        if (!success)
            return BadRequest(new { errors });

        return Ok(result);
    }
}