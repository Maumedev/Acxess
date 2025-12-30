namespace Acxess.Marketing.Domain.Entities;

public class Promotion
{
    private Promotion()
    {
    }

    private Promotion(int idTenant, string name, int createdByUser, decimal? discountAmount = null, decimal? discountPercentage = null, bool requiresCoupon = false, int? minItemsPurchase = null, DateTime? availableFrom = null, DateTime? availableTo = null, bool isActive = true)
    {
        IdTenant = idTenant;
        Name = name;
        CreatedByUser = createdByUser;
        DiscountAmount = discountAmount;
        DiscountPercentage = discountPercentage;
        RequiresCoupon = requiresCoupon;
        MinItemsPurchase = minItemsPurchase;
        AvailableFrom = availableFrom;
        AvailableTo = availableTo;
        IsActive = isActive;
    }

    public int IdPromotion { get; private set; }
    public int IdTenant { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public decimal? DiscountAmount { get; private set; }
    public decimal? DiscountPercentage { get; private set; }
    public bool RequiresCoupon { get; private set; } = false;
    public int? MinItemsPurchase { get; private set; }
    public DateTime? AvailableFrom { get; private set; }
    public DateTime? AvailableTo { get; private set; }
    public bool IsActive { get; private set; } = true;

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public int CreatedByUser { get; private set; }

    
}
