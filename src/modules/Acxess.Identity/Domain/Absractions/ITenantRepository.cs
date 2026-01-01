using Acxess.Identity.Domain.Entities;

namespace Acxess.Identity.Domain.Absractions;

public interface ITenantRepository
{
    void Add(Tenant tenant);
}
