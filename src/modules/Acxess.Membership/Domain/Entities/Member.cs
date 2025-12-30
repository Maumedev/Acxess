using Acxess.Shared.Abstractions;

namespace Acxess.Membership.Domain.Entities;

public class Member : IHasTenant
{
    public int IdMember { get; private set; }
    public int IdTenant { get; private set; }
    public string FirtsName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    public bool IsDeleted { get; private set; } = false;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public int CreatedByUser { get; private set; }

    private Member()
    {
        
    }

    private Member(int tenantId, string firtsName, string lastName, string? email, string? phone, int createdByUser)
    {
        IdTenant = tenantId;
        FirtsName = firtsName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        CreatedByUser = createdByUser;
    }
}
