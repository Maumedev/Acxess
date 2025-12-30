
namespace Acxess.Billing.Domain.Entities;

public class MemberTransaction
{
    public int IdMemberTransaction { get; private set; }
    public int IdTenant { get; private set; }
    public int? IdMember { get; private set; }
    public int IdPaymentMethod { get; private set; }
    public decimal Total { get; private set; }
    public string? Notes { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public int CreatedByUser { get; private set; }

    private MemberTransaction(){ }
    private MemberTransaction(int idTenant,  int idPaymentMethod, decimal total, string? notes, int createdByUser, int? idMember = null)
    {
        IdTenant = idTenant;
        IdMember = idMember;
        IdPaymentMethod = idPaymentMethod;
        Total = total;
        Notes = notes;
        CreatedByUser = createdByUser;
    }
}
