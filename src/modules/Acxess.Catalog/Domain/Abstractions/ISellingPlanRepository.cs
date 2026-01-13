using Acxess.Catalog.Domain.Entities;

namespace Acxess.Catalog.Domain.Abstractions;

public interface ISellingPlanRepository
{
    void Add(SellingPlan sellingPlan);
    Task<SellingPlan?> GetById(int id, CancellationToken cancellationToken);

    Task<SellingPlan?> GetByIdWithTiersAsync(int id, CancellationToken cancellationToken);

}
