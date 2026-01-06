using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

            if (context.HttpContext.Request.Method == "POST")
            {
                var message = (env.IsDevelopment() || env.IsEnvironment("Localhost"))
                    ? $"{executedContext.Exception.Message}" 
                    : "Ocurrió un error inesperado. Por favor intente más tarde.";

                executedContext.ModelState.AddModelError(string.Empty, message);
                executedContext.ExceptionHandled = true;

                var isAjax = context.HttpContext.Request.Headers.ContainsKey("HX-Request") || 
                             context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

                if (isAjax)
                {
                    string viewName = "_Form";

                    if (context.ActionDescriptor is PageActionDescriptor pageDescriptor)
                    {
                        var pageName = Path.GetFileNameWithoutExtension(pageDescriptor.RelativePath);
                        viewName = $"_{pageName}";
                    }

                    var result = new PartialViewResult
                    {
                        ViewName = viewName,
                        ViewData = new ViewDataDictionary<object>(new EmptyModelMetadataProvider(), executedContext.ModelState)
                        {
                            Model = executedContext.HandlerInstance // Recuperamos tu PageModel con los datos
                        }
                    };

                    executedContext.Result = result;
                }
                else
                {
                    executedContext.Result = new PageResult();
                }
            }
        }
    }

    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        => Task.CompletedTask;
}
