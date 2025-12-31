using System;

namespace Acxess.Identity.Domain.Entities;

public class Tenant
{
    public int IdTenant { get; private set; }
    public string Name { get; private set; }
    public string? Logo { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    private Tenant(string name)
    {
        Name = name;
    }

    public static Tenant Create(string name)
        => new(name);
    
    public void SetLogo(string logo)
        => Logo = logo;

    public void Desactive()
    {
        if (!IsActive)
            // return Result.Failure("El tenant ya a sido desactivado.");
            throw new InvalidOperationException("El tenant ya a sido desactivado.");

        IsActive = false;
    }
}
