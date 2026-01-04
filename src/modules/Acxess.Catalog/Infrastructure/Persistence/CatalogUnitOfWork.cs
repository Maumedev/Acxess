using Acxess.Catalog.Domain.Abstractions;
using Acxess.Infrastructure.Persistence;
using Acxess.Shared.ResultManager;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Catalog.Infrastructure.Persistence;

public class CatalogUnitOfWork(
    CatalogModuleContext dbContext
) : ICatalogUnitOfWork
{
    public async Task<Result> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (DbUpdateException ex)
        {
            var error = SqlExceptionParser.Parse(ex);

            return Result.Failure(error ?? Error.Failure("Database.Error", "Error inesperado de base de datos."));
        }
    }
}
