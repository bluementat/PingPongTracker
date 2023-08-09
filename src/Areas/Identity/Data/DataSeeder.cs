using Microsoft.AspNetCore.Identity;
using PingPongTracker.Models;

namespace PingPongTracker;

public class DataSeeder
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DataSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedData()
    {
        await SeedRoles();
        await SeedUsers();
    }

    private async Task SeedRoles()
    {
        if (!await _roleManager.RoleExistsAsync("Admin"))
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        if (!await _roleManager.RoleExistsAsync("User"))
        {
            await _roleManager.CreateAsync(new IdentityRole("User"));
        }
    }

    private async Task SeedUsers()
    {
        if (await _userManager.FindByNameAsync("Admin") == null)
        {
            await _userManager.CreateAsync(new ApplicationUser()
            {
                UserName = "Admin",           
                Email = "one@one.com",
                EmailConfirmed = true,
                Active = true
            },
            "Password1!");
        }
        // Does the Admin user have the Admin role?
        var adminUser = await _userManager.FindByNameAsync("Admin") ?? throw new Exception("The admin user was not found.");
        if (!await _userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await _userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
