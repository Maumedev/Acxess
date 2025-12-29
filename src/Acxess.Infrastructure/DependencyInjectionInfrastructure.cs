using Acxess.Core.Modules.Identity.Entities;
using Acxess.Infrastructure.Middlewares;
using Acxess.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Acxess.Infrastructure;

public static class DependencyInjectionInfrastructure
{

    public static async Task<WebApplication> PrepareDbData(this WebApplication app)
    {
        await DataSeeds.InitData(app);
        return app;
    }

    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        => services
        .AddDatabaseConnection(configuration)
        .AddIdentity()
        .AddLogging()
        .AddMiddlewares();


    private static IServiceCollection AddDatabaseConnection(this IServiceCollection services, IConfiguration configuration)
    {
        var connStr = configuration.GetConnectionString("Default");
        services.AddDbContext<AcxessContext>(options => options.UseSqlServer(connStr));
        return services;
    }

    private static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 1;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<AcxessContext>()
        .AddDefaultTokenProviders();

        return services;
    }

    private static IServiceCollection AddMiddlewares(this IServiceCollection services)
        => services
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddProblemDetails();

}
