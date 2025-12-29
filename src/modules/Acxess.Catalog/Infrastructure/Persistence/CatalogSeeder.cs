using Acxess.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Catalog.Infrastructure.Persistence;

public class CatalogSeeder(CatalogModuleContext context) : IDataSeeder
{
    public async Task SeedAsync()
    {
        await context.Database.MigrateAsync();
    }
}
