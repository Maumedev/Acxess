using System;
using Acxess.Identity.Domain.Entities;
using Acxess.Shared.Abstractions;
using Acxess.Shared.Constants;
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

        string[] roleNames = [ApplicationRoles.SuperAdmin, ApplicationRoles.Admin, ApplicationRoles.User];
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
                await roleManager.CreateAsync(new IdentityRole(roleName));
        }

        string systemEmail = "system@system.com";
        if (await userManager.FindByEmailAsync(systemEmail) == null)
        {
            var user = new ApplicationUser
            {
                UserName = "system",
                Email = systemEmail,
                FullName = "User System",
                IsActive = true,
                EmailConfirmed = true
            };
            string pass = configuration["SystemUser:Password"] ?? throw new InvalidOperationException("System user password is not configured.");
            var result = await userManager.CreateAsync(user, pass);
            if (result.Succeeded) await userManager.AddToRoleAsync(user, ApplicationRoles.SuperAdmin);
        }
    }
}
