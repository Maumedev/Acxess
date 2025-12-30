
using Acxess.Billing.Domain.Enums;

namespace Acxess.Billing.Domain.Entities;

public class MemberTransactionDetail
{
    public int IdMemberTransactionDetail { get; private set; }
    public int IdMemberTransaction { get; private set; }
    public int? IdSubscription { get; private set; }
    public int? IdItem { get; private set; }
    public ItemTransactionType ItemTransactionType { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public int Amount { get; private set; }
    public decimal Price { get; private set; }
    public decimal Total { get; private set; }

    private MemberTransactionDetail() { }
    private MemberTransactionDetail(int idMemberTransaction, ItemTransactionType itemTransactionType, string description, int amount, decimal price, decimal total, int? idSubscription = null, int? idItem = null)
    {
        IdMemberTransaction = idMemberTransaction;
        IdSubscription = idSubscription;
        IdItem = idItem;
        ItemTransactionType = itemTransactionType;
        Description = description;
        Amount = amount;
        Price = price;
        Total = total;
    }
      
}
