using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Acxess.Web.Filters;

public class PageExceptionFilter(ILogger<PageExceptionFilter> logger, IHostEnvironment env) : IAsyncPageFilter
{
    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        var executedContext = await next();

        if (executedContext.Exception != null)
        {
            logger.LogError(executedContext.Exception, "Error no controlado en Razor Page: {Message}", executedContext.Exception.Message);
            var message = (env.IsDevelopment() || env.IsEnvironment("Localhost"))
                ? $"{executedContext.Exception.Message}" 
                : "Ocurrió un error inesperado. Por favor intente más tarde.";

            executedContext.ModelState.AddModelError(string.Empty, message);
            executedContext.ExceptionHandled = true;

            executedContext.Result = new PartialViewResult()
            {
                ViewName = "_ErrorState",
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(),executedContext.ModelState)
                {
                    Model = message
                }
            };
        }
    }

    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        => Task.CompletedTask;
}
