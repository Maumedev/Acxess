using Acxess.Catalog.Domain.Entities;

namespace Acxess.Catalog.Domain.Abstractions;

public interface ISellingPlanRepository
{
    void Add(SellingPlan sellingPlan);
}
