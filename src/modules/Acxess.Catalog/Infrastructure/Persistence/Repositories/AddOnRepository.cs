using Acxess.Catalog.Domain.Abstractions;
using Acxess.Catalog.Domain.Entities;

namespace Acxess.Catalog.Infrastructure.Persistence.Repositories;

public class AddOnRepository(
    CatalogModuleContext context
) : IAddOnRepository
{
    public void Add(AddOn addOn)
    {
        context.AddOns.Add(addOn);
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
