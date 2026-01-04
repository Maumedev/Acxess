using Acxess.Identity.Domain.Absractions;
using Acxess.Infrastructure.Persistence;
using Acxess.Shared.ResultManager;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Identity.Infrastructure.Persistence;

public class IdentityUnitOfWork(
    IdentityModuleContext dbContext
) : IIdentityUnitOfWork
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
