using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Seed;

public class Seeder(DataContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
{
    public async Task<bool> SeedRole()
    {
        var newRoles = new List<IdentityRole>()
        {
            new IdentityRole(Roles.Admin),
            new IdentityRole(Roles.Trainer),
            new IdentityRole(Roles.User)
        };
        var existing = roleManager.Roles.ToList();
        foreach (var item in newRoles)
        {
            if ( existing.Exists(e => e.Name != item.Name))
            {
                await roleManager.CreateAsync(item);
            }
        }
        return true;
    }
    public async Task<bool> SeedUser()
    {
        var existing = await userManager.FindByNameAsync("admin");
        if (existing != null) return false;
        
        var identity = new IdentityUser()
        {
            UserName = "admin",
        };

        var result = await userManager.CreateAsync(identity, "hello123");
        await userManager.AddToRoleAsync(identity, Roles.Admin);
        return result.Succeeded;
    }
}
