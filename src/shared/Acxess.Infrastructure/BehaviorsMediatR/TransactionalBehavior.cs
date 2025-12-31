using System.Transactions;
using Acxess.Shared.Exceptions;
using Acxess.Shared.ResultManager;
using MediatR;

namespace Acxess.Infrastructure.BehaviorsMediatR;

public class TransactionalBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        try
        {
            var response = await next();

            if (response.IsSuccess)
            {
                scope.Complete();
            }

            return response;
        }
        catch(IntegrationEventException ex)
        {
            var failureMethod = typeof(TResponse)
                .GetMethod("Failure", [typeof(Error)]) 
                ?? typeof(Result).GetMethod("Failure", [typeof(Error)]);

            return (TResponse)failureMethod!.Invoke(null, [ex.Error])!;
        }
        catch (Exception )
        {
            
            throw;
        }
    }
}
