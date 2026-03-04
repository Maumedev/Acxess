using Acxess.Shared.ResultManager;

namespace Acxess.Shared.Abstractions;

public interface IImageStorageService
{
    Task<Result<string>> SaveImageAsync(string base64Image, string fileName, CancellationToken cancellationToken = default);
    Task<Result> DeleteImageAsync(string photoUrl, CancellationToken cancellationToken = default);
}