using Acxess.Infrastructure.Persistence;
using Acxess.Shared.ResultManager;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Acxess.Infrastructure.BehaviorsMediatR;
/*
 * Catch global database exceptions from Entity Framework Contexts
 */
public class DatabaseExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        try
        {
            return await next(); 
        }
        catch (DbUpdateException ex)
        {
            if (!typeof(Result).IsAssignableFrom(typeof(TResponse))) throw;
            
            var error = SqlExceptionParser.Parse(ex) 
                        ?? Error.Failure("Database.Error", "Error inesperado al guardar en la base de datos.");

            var failureMethod = typeof(TResponse).GetMethod("Failure", [typeof(Error)]) 
                                ?? typeof(Result).GetMethod("Failure", [typeof(Error)]);
            if (failureMethod is not null)
            {
                return (TResponse)failureMethod.Invoke(null, [error])!;
            }
            
            throw;
        }
    }
}