using System;

namespace Acxess.Catalog.Domain.Entities;

public class PlanAccessTiers
{
    public PlanAccessTiers(int accessTierId, int sellingPlanId)
    {
        IdAccessTier = accessTierId;
        IdSellingPlan = sellingPlanId;
    }

    public int IdPlanAccessTier { get; private set; }
    public int IdAccessTier { get; private set; }
    public int IdSellingPlan { get; private set; }
}
