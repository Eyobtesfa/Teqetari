// Api/NameIdentifierUserIdProvider.cs
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace TeqetariApi.Api;

public class NameIdentifierUserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection) =>
        connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}