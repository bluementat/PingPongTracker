using Microsoft.AspNetCore.Identity;

namespace PingPongTracker.Models;

public class ApplicationUser : IdentityUser
{
    public bool Active { get; set; }
}

