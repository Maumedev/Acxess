using Acxess.Catalog.Domain.Entities;

namespace Acxess.Catalog.Domain.Abstractions;

public interface IAccessTierRepository
{
    List<AccessTier> GetAccessTiers();
}
