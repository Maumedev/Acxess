using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Acxess.Web.Filters;

public class PageExceptionFilter(ILogger<PageExceptionFilter> logger, IHostEnvironment env) : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        logger.LogError(context.Exception, "Error no controlado en Razor Page: {Message}", context.Exception.Message);

        if (context.HttpContext.Request.Method == "POST")
        {
            var message = env.IsDevelopment() 
                ? $"DEBUG ERROR: {context.Exception.Message}" 
                : "Ocurrió un error inesperado. Por favor intente más tarde.";

            context.ModelState.AddModelError(string.Empty, message);
            context.ExceptionHandled = true;
            
            context.Result = new PageResult();
        }
    }
}
