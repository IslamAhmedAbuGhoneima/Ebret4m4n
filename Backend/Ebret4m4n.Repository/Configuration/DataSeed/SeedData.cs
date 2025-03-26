using Ebret4m4n.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ebret4m4n.Repository.Configuration.DataSeed;

public class SeedData
{
    public static async Task CreateAdminUser(IServiceProvider serviceProvider,
        IConfiguration configuration)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string adminEmail = configuration["AdminUser:email"];
        string adminPassword = configuration["AdminUser:password"];

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                FirstName = "وزاره",
                LastName = "الصحه",
                Governorate = "القريه الذكيه",
                UserName = adminEmail,
                Email = adminEmail,
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);

            if (result.Succeeded)
                await userManager.AddToRoleAsync(adminUser, "admin");
            else
                Console.WriteLine("Failed to create admin user.");
        }
        else
        {
            Console.WriteLine("Admin user already exists.");
        }
    }
}
