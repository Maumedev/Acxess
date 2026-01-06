using Acxess.Catalog.Domain.Entities;

namespace Acxess.Catalog.Domain.Abstractions;

public interface IAccessTierRepository
{
    void Add(AccessTier accessTier);
    void Update(AccessTier accessTier);
    Task<AccessTier?> GetById(int id, CancellationToken cancellationToken);
}
