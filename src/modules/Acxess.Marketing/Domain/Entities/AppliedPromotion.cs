namespace Acxess.Marketing.Domain.Entities;

public class AppliedPromotion
{
    private AppliedPromotion() {}

    private AppliedPromotion(int idMemberTransactionDetail, decimal appliedAmount, int? idPromotion = null, int? idCoupon = null, string? notes = null)
    {
        IdMemberTransactionDetail = idMemberTransactionDetail;
        IdPromotion = idPromotion;
        IdCoupon = idCoupon;
        AppliedAmount = appliedAmount;
        Notes = notes;
    }

    public int IdAppliedPromotion { get; private set; }
    public int IdMemberTransactionDetail { get; private set; }
    public int? IdPromotion { get; private set; }
    public int? IdCoupon { get; private set; }
    public decimal AppliedAmount { get; private set; }
    public string? Notes { get; private set; }
    
}
