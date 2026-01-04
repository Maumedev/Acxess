using Acxess.Catalog.Domain.Abstractions;
using Acxess.Catalog.Domain.Entities;

namespace Acxess.Catalog.Infrastructure.Persistence.Repositories;

public class SellingPlanRepository(
    CatalogModuleContext dbContext
) : ISellingPlanRepository
{
    public void Add(SellingPlan sellingPlan)
    {
        dbContext.SellingPlans.Add(sellingPlan);
    }
}
