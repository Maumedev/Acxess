using Acxess.Core.Modules.Identity.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Acxess.Infrastructure.Persistence;

public class DataSeeds
{
    public async static Task InitData(WebApplication app)
    {
        string[] roleNames = ["Admin", "Agent"];

        using var scope = app.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var context = serviceProvider.GetRequiredService<AcxessContext>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        try
        {
            context.Database.Migrate();
            Console.WriteLine("--> Migración aplicada.");

            foreach (var roleName in roleNames)
                if (!await roleManager.RoleExistsAsync(roleName))
                    await roleManager.CreateAsync(new IdentityRole(roleName));


            string systemEmail = "system@gmail.com";
            var user = await userManager.FindByEmailAsync(systemEmail);

            if (user == null)
            {
                var userSystem = new ApplicationUser
                {
                    UserName = "system", 
                    Email = systemEmail,
                    TenantId = null,
                    FullName = "User System",
                    Active = true,
                    EmailConfirmed = true 
                };

                string pass = configuration["SystemUser:Password"] ?? "Acxess123!#";

                var result = await userManager.CreateAsync(userSystem, pass);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userSystem, "Admin");
                    Console.WriteLine("--> Usuario System creado con éxito.");
                }
            }

        }
        catch (System.Exception ex)
        {
            
            Console.WriteLine($"--> Error registrando datos semilla y migraciones {ex.Message}");
        }
    }
}
