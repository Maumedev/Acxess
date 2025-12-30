using Acxess.Shared.Abstractions;

namespace Acxess.Catalog.Domain.Entities;

public class AddOn : IHasTenant
{


    public int IdAddOn { get; private set; }
    public int IdTenant { get; private set; }
    public string AddOnKey { get; private set; }
    public string Name { get; private set; }
    public string Price { get; private set; }
    public bool ShowInCheckout { get; private set; }

    public AddOn(int tenantId, string addOnKey, string name, string price, bool showInCheckout = false)
    {
        IdTenant = tenantId;
        AddOnKey = addOnKey;
        Name = name;
        Price = price;
        ShowInCheckout = showInCheckout;
    }
}
