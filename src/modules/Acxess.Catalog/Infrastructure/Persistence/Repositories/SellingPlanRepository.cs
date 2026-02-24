using Acxess.Catalog.Domain.Abstractions;
using Acxess.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Catalog.Infrastructure.Persistence.Repositories;

public class SellingPlanRepository(
    CatalogModuleContext dbContext
) : ISellingPlanRepository
{
    public void Add(SellingPlan sellingPlan)
    {
        dbContext.SellingPlans.Add(sellingPlan);
    }

    public async Task<SellingPlan?> GetById(int id, CancellationToken cancellationToken)
    {
        return await dbContext.SellingPlans.FindAsync([id], cancellationToken);
    }

    public async Task<SellingPlan?> GetByIdWithTiersAsync(int id, CancellationToken cancellationToken)
    {
       return await dbContext.SellingPlans
        .Include(sp => sp.AccessTiers)
        .FirstOrDefaultAsync(sp => sp.IdSellingPlan == id, cancellationToken);
    }
}
