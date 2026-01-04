using Acxess.Shared.ResultManager;

namespace Acxess.Shared.Abstractions;

public interface IUnitOfWork
{
    Task<Result> SaveChangesAsync(CancellationToken cancellationToken = default);   
}
