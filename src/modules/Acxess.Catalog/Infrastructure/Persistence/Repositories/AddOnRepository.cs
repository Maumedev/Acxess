using Acxess.Catalog.Domain.Abstractions;
using Acxess.Catalog.Domain.Entities;

namespace Acxess.Catalog.Infrastructure.Persistence.Repositories;

public class AddOnRepository(
    CatalogModuleContext dbContext
) : IAddOnRepository
{
    public void Add(AddOn addOn)
    {
        dbContext.AddOns.Add(addOn);
    }
}
