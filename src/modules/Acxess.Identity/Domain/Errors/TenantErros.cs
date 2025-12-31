using Acxess.Shared.ResultManager;

namespace Acxess.Identity.Domain.Errors;

public static class TenantErros
{
    public static readonly Error AlreadyDesactivated = Error.Conflict(
        "Identity.Tenant.AlreadyDesactivated", 
        "El tenant ya a sido desactivado.");
}
