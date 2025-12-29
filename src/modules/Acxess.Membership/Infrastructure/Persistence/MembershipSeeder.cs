using System;
using Acxess.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Membership.Infrastructure.Persistence;

public class MembershipSeeder(MembershipModuleContext context) : IDataSeeder
{
    public async Task SeedAsync()
    {
        await context.Database.MigrateAsync();
    }
}
