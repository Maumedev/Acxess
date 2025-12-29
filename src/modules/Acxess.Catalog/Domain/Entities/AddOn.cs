using System;

namespace Acxess.Catalog.Domain.Entities;

public class AddOn
{


    public int AddOnId { get; private set; }
    public int TenantId { get; private set; }
    public string AddOnKey { get; private set; }
    public string Name { get; private set; }
    public string Price { get; private set; }
    public bool ShowInCheckout { get; private set; }

    public AddOn(int tenantId, string addOnKey, string name, string price, bool showInCheckout = false)
    {
        TenantId = tenantId;
        AddOnKey = addOnKey;
        Name = name;
        Price = price;
        ShowInCheckout = showInCheckout;
    }
}
