using System.Text.RegularExpressions;
using Acxess.Shared.Abstractions;
using Acxess.Shared.ResultManager;
using Microsoft.AspNetCore.Hosting;

namespace Acxess.Infrastructure.Services;

public partial class LocalDiskStorageService(IWebHostEnvironment env) : IImageStorageService
{
    public async Task<Result<string>> SaveImageAsync(string base64Image, string fileName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(base64Image)) 
            return string.Empty;
        
        var match = HeadBase64Regex().Match(base64Image);
        var base64Data = match.Success ? match.Groups["data"].Value : base64Image;
        
        var imageBytes = Convert.FromBase64String(base64Data);
        
        var folderName = Path.Combine("uploads", "members");
        var folderPath = Path.Combine(env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), folderName);
        
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        
        var uniqueFileName = $"{Guid.NewGuid()}_{fileName}.jpg";
        var physicalPath = Path.Combine(folderPath, uniqueFileName);
        await File.WriteAllBytesAsync(physicalPath, imageBytes, cancellationToken);

        return $"/{folderName}/{uniqueFileName}".Replace("\\", "/");
    }

    public async Task<Result> DeleteImageAsync(string photoUrl, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(photoUrl))
            return Result.Success();

        var relativePath = photoUrl.TrimStart('/');
        var physicalPath = Path.Combine(env.WebRootPath, relativePath);

        if (File.Exists(physicalPath))
        {
            File.Delete(physicalPath);
        }

        await Task.CompletedTask;
        return  Result.Success();
    }

    [GeneratedRegex(@"data:image/(?<type>.+?);base64,(?<data>.+)")]
    private static partial Regex HeadBase64Regex();
}