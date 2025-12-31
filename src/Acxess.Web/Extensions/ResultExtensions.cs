using Acxess.Shared.ResultManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Acxess.Web.Extensions;

public static class ResultExtensions
{
    public static IActionResult HandleResult<T>(
        this Result<T> result,
        ModelStateDictionary modelState,
        Func<T, IActionResult> onSuccess,
        Func<IActionResult> onFailure)
    {
        if (result.IsSuccess)
        {
            return onSuccess(result.Value);
        }

        // Mapeamos el Error del Dominio al ModelState de Razor
        modelState.AddModelError(string.Empty, result.Error.Description);

        return onFailure();
    }
    
    // Sobrecarga para Result sin valor (void)
    public static IActionResult HandleResult(
        this Result result,
        ModelStateDictionary modelState,
        Func<IActionResult> onSuccess,
        Func<IActionResult> onFailure)
    {
        if (result.IsSuccess) return onSuccess();
        
        modelState.AddModelError(string.Empty, result.Error.Description);
        return onFailure();
    }
}
