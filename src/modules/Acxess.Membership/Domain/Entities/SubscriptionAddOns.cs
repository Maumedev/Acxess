namespace Acxess.Membership.Domain.Entities;

public class SubscriptionAddOns
{

    private SubscriptionAddOns(){}

    public SubscriptionAddOns(int addOnId, int subscriptionId, decimal priceSnapshot)
    {
        IdAddOn = addOnId;
        IdSubscription = subscriptionId;
        PriceSnapshot = priceSnapshot;
    }

    public int IdSubscriptionAddOn { get; private set; }
    public int IdAddOn { get; private set; }
    public int IdSubscription { get; private set; }
    public decimal PriceSnapshot { get; private set; }
    
}
