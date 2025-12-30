using Acxess.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Billing.Infrastrucutre.Persistence;

public class BillingSeeder(BillingModuleContext context) : IDataSeeder
{
    public async Task SeedAsync()
    {
        await context.Database.MigrateAsync();
    }
}
