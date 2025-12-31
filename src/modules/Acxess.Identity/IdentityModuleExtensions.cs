using Acxess.Identity.Domain.Entities;
using Acxess.Identity.Infrastructure.Identity;
using Acxess.Identity.Infrastructure.Persistence;
using Acxess.Shared.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Acxess.Identity;

public static class IdentityModuleExtensions
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");

        services.AddDbContext<IdentityModuleContext>(options =>
        {
            options.UseSqlServer(connectionString, sqlOptions =>
                sqlOptions.MigrationsHistoryTable("__IdentityMigrationsHistory", "Identity")
            );
        });

        services.AddApplicationIdentity();

        services.AddScoped<IDataSeeder, IdentitySeeder>();

        return services;
    }

    private static IServiceCollection AddApplicationIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false; 
            options.Password.RequiredLength = 1;            
            options.Password.RequiredUniqueChars = 0;        
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<IdentityModuleContext>()
        .AddDefaultTokenProviders()
        .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>();

        return services;
    }
}
