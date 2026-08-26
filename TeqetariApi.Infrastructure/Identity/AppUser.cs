using Microsoft.AspNetCore.Identity;

namespace TeqetariApi.Infrastructure.Identity;


public class AppUser : IdentityUser
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }
    public bool IsActive { get; set; } = true;
    public string UserType { get; set; } = string.Empty;
}