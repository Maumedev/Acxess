namespace Acxess.Membership.Domain.Entities;

public class Subscription
{

    private Subscription(){}
    private Subscription(int tenantId, int ownerMemberId, int sellingPlanId, decimal priceSnapshot, int createdByUser, string? notes = null)
    {
        IdTenant = tenantId;
        IdMemberOwner = ownerMemberId;
        IdSellingPlan = sellingPlanId;
        PriceSnapshot = priceSnapshot;
        CreatedByUser = createdByUser;
        Notes = notes;
    }

    public int IdSubscription { get; private set; }
    public int IdTenant { get; private set; }
    public int IdMemberOwner { get; private set; }
    public int IdSellingPlan { get; private set; }
    public bool IsAcive { get; private set; } = true;
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public decimal PriceSnapshot { get; private set; }
    public string? Notes { get; private set; }
    public DateTime CreatedAt { get; private set; } =  DateTime.UtcNow;
    public int CreatedByUser { get; private set; }

    
}
