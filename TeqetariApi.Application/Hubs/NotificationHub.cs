using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace TeqetariApi.Application.Hubs;

[Authorize]
public class NotificationHub : Hub
{
    // Groups each connection by the user's own AppUserId, so we can push
    // a message to "this specific user" regardless of which tab/device
    // they're connected from.
    public override async Task OnConnectedAsync()
    {
        var appUserId = Context.UserIdentifier; // requires user-id resolution — see step 4
        if (!string.IsNullOrEmpty(appUserId))
            await Groups.AddToGroupAsync(Context.ConnectionId, appUserId);

        await base.OnConnectedAsync();
    }
}