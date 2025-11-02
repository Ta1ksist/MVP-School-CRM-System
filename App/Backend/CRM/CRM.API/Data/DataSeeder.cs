using Microsoft.AspNetCore.Identity;

namespace CRM.API.Data;

public class DataSeeder
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
    {
        var roles = new[] { "Teacher", "Director", "Admin" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole<Guid>(role));
        }
    }
}