using System;
using Acxess.Catalog.Domain.Enums;

namespace Acxess.Catalog.Domain.Entities;

public class SellingPlan
{
    public int IdSellingPlan { get; private set; }
    public int IdTenant { get; private set; }
    public string Name { get; private set; }
    public int TotalMembers { get; private set; }
    public int DurationInValue { get; private set; }
    public DurationUnit DurationUnit { get; private set; }
    public decimal Price { get; private set; }
    public bool IsActive { get; private set; } = true;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public int CreatedByUser { get; private set; }

    public SellingPlan(int tenantId, string name, int totalMembers, int durationInValue, DurationUnit durationUnit, decimal price, int createdByUser)
    {
        IdTenant = tenantId;
        Name = name;
        TotalMembers = totalMembers;
        DurationInValue = durationInValue;
        DurationUnit = durationUnit;
        Price = price;
        CreatedByUser = createdByUser;
    }

}
