// Infrastructure/Services/Notifications/NotificationService.cs
using Microsoft.AspNetCore.SignalR;
using TeqetariApi.Application.Hubs;

namespace TeqetariApi.Infrastructure.Services.Notifications;

public class NotificationService(IHubContext<NotificationHub> hub) : INotificationService
{
    public async Task NotifyUserAsync(string appUserId, string eventType, object payload)
    {
        await hub.Clients.Group(appUserId).SendAsync("notification", new { type = eventType, payload });
    }
}