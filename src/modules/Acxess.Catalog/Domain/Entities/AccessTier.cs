namespace Acxess.Catalog.Domain.Entities;

public class AccessTier
{
    public int IdAccessTier { get; private set; }
    public int IdTenant { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; } = true;

    private AccessTier(int tenantId, string name, string? description = null)
    {
        IdTenant = tenantId;
        Name = name;
        Description = description;
    }

    public static AccessTier Create(int tenantId, string name, string? description = null)
    {
        return new AccessTier(tenantId, name, description);
    }
}
