using Acxess.Shared.ResultManager;

namespace Acxess.Shared.Exceptions;

public class IntegrationEventException (Error error) : Exception(error.Description)
{
    public Error Error { get; } = error;
}
