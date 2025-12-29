using System;

namespace Acxess.Catalog.Domain.Entities;

public class PlanAccessTiers
{
    public PlanAccessTiers(int accessTierId, int sellingPlanId)
    {
        AccessTierId = accessTierId;
        SellingPlanId = sellingPlanId;
    }

    public int PlanAccessTierId { get; private set; }
    public int AccessTierId { get; private set; }
    public int SellingPlanId { get; private set; }
}
