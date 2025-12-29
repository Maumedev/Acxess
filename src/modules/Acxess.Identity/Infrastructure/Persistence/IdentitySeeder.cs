using System;
using Acxess.Identity.Domain.Entities;
using Acxess.Shared.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Acxess.Identity.Infrastructure.Persistence;

public class IdentitySeeder(
    IdentityModuleContext context,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IConfiguration configuration) : IDataSeeder
{
    public async Task SeedAsync()
    {
        await context.Database.MigrateAsync();

        string[] roleNames = ["Admin", "Agent"];
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
                await roleManager.CreateAsync(new IdentityRole(roleName));
        }

        string systemEmail = "system@gmail.com";
        if (await userManager.FindByEmailAsync(systemEmail) == null)
        {
            var user = new ApplicationUser
            {
                UserName = "system",
                Email = systemEmail,
                FullName = "User System",
                Active = true,
                EmailConfirmed = true
            };
            string pass = configuration["SystemUser:Password"] ?? "Acxess123!#";
            var result = await userManager.CreateAsync(user, pass);
            if (result.Succeeded) await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
