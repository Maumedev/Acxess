using System.Threading.Tasks;
using Acxess.Catalog.Domain.Abstractions;
using Acxess.Catalog.Domain.Entities;

namespace Acxess.Catalog.Infrastructure.Persistence.Repositories;

public class AccessTierRepository(
    CatalogModuleContext context
) : IAccessTierRepository
{
    public void Add(AccessTier accessTier)
    {
        context.AccessTiers.Add(accessTier);
    }

    public async Task<AccessTier?> GetById(int id, CancellationToken cancellationToken)
    {
        return await context.AccessTiers.FindAsync([id], cancellationToken);
    }

    public void Update(AccessTier accessTier)
    {
        context.AccessTiers.Update(accessTier);
    }
}
