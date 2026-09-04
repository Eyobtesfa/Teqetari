// Application/Interfaces/INotificationService.cs
public interface INotificationService
{
    Task NotifyUserAsync(string appUserId, string eventType, object payload);
}