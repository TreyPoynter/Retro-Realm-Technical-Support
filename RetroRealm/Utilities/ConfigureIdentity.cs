using Microsoft.AspNetCore.Identity;
using RetroRealm.Models;

namespace RetroRealm.Utilities
{
    public class ConfigureIdentity
    {
        public static async Task CreateAdminUserASync(IServiceProvider provider)
        {
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = provider.GetRequiredService<UserManager<User>>();

            string userName = "admin";
            string password = "PassW0rd#1";
            string roleName = "Admin";

            if(await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
            if(await userManager.FindByNameAsync(userName) == null)
            {
                User adminUser = new User() { UserName = userName };
                var result = await userManager.CreateAsync(adminUser, password);
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, roleName);
                }
            }
        }
    }
}
