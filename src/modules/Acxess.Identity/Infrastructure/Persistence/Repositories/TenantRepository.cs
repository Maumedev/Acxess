using Acxess.Identity.Domain.Absractions;
using Acxess.Identity.Domain.Entities;

namespace Acxess.Identity.Infrastructure.Persistence.Repositories;

public class TenantRepository(
    IdentityModuleContext dbContext
) : ITenantRepository
{
    public void Add(Tenant tenant)
    {
        dbContext.Tenants.Add(tenant);
    }
}
