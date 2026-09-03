using TeqetariApi.Application.DTOs.Create;
using TeqetariApi.Application.DTOs.Response;
using TeqetariApi.Application.DTOs.Update;

namespace TeqetariApi.Application.Interfaces;

public interface IHireRequestService
{
    Task<(bool Success, HireRequestResponseDto? Result, IEnumerable<string> Errors)> SendHireRequestAsync(string appUserId, HireRequestDto dto, CancellationToken ct);
    Task<(bool Success, IEnumerable<HireRequestResponseDto> Result, IEnumerable<string> Errors)> GetSentHireRequestsAsync(string appUserId, CancellationToken ct);
    Task<(bool Success, IEnumerable<HireRequestResponseDto> Result, IEnumerable<string> Errors)> GetReceivedHireRequestsAsync(string appUserId, CancellationToken ct);
    Task<(bool Success, string? Error)> RespondToHireRequestAsync(string appUserId, int hireRequestId, HireRequestUpdateDto dto, CancellationToken ct);
}