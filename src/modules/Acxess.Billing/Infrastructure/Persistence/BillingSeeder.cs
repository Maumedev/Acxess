using Acxess.Billing.Domain.Entities;
using Acxess.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Billing.Infrastructure.Persistence;

public class BillingSeeder(BillingModuleContext context) : IDataSeeder
{
    public async Task SeedAsync()
    {
        await context.Database.MigrateAsync();

        if (!await context.PaymentMethods.AnyAsync(p => p.Method == "Efectivo" && p.IdTenant == null))
        {
            context.PaymentMethods.Add(PaymentMethod.Create("Efectivo"));
        }

        if (!await context.PaymentMethods.AnyAsync(p => p.Method == "Transferencia" && p.IdTenant == null))
        {
            context.PaymentMethods.Add(PaymentMethod.Create("Transferencia"));
        }

        await context.SaveChangesAsync();
    }
}
