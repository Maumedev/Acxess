using Acxess.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Marketing.Infrastructure.Persistence;

public class MarketingSeeders(MarketingModuleContext context) : IDataSeeder
{
    public async Task SeedAsync()
    {
        await context.Database.MigrateAsync();
    }
}
