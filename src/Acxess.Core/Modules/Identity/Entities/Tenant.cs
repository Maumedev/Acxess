using System;
using ROP;

namespace Acxess.Core.Modules.Identity.Entities;

public class Tenant
{
    public int TenantId { get; private set; }
    public string Name { get; private set; }
    public string? Logo { get; private set; }
    public bool Active { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    private Tenant(string name)
    {
        Name = name;
    }

    public static Tenant Create(string name)
        => new(name);
    
    public void SetLogo(string logo)
        => Logo = logo;

    public Result<Unit> Desactive()
    {
        if (!Active)
            return Result.Failure("El tenant ya a sido desactivado.");

        Active = false;
        return Result.Success();
    }
}
