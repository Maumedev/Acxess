namespace Acxess.Shared.ResultManager;

public record Error(string Code, string Description, ErrorType Type)
{
    public static Error None => new(string.Empty, string.Empty, ErrorType.Failure);
    public static Error Failure(string code, string desc) => new(code, desc, ErrorType.Failure);
    public static Error Validation(string code, string desc) => new(code, desc, ErrorType.Validation);
    public static Error NotFound(string code, string desc) => new(code, desc, ErrorType.NotFound);
    public static Error Conflict(string code, string desc) => new(code, desc, ErrorType.Conflict);
}