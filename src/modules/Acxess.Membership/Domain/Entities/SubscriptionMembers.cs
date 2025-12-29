namespace Acxess.Membership.Domain.Entities;

public class SubscriptionMembers
{
    private SubscriptionMembers(){}
    private SubscriptionMembers(int memberId, int subscriptionId, bool owner)
    {
        IdMember = memberId;
        IdSubscription = subscriptionId;
        Owner = owner;
    }

    public int IdSubscriptionMember { get; private set; }
    public int IdMember { get; private set; }
    public int IdSubscription { get; private set; }
    public bool Owner { get; private set; }
}
