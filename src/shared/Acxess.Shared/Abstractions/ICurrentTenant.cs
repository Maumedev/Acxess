namespace Acxess.Shared.Abstractions;

public interface ICurrentTenant
{
    int? Id { get; }
    bool IsAvailable { get; }
}
