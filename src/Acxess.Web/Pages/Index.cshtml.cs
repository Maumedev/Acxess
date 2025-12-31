using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Acxess.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }

    public IActionResult OnGetDameLaHora()
    {
        // Simulamos un retraso de 1 segundo para ver el spinner de carga
        System.Threading.Thread.Sleep(1000); 

        // Retornamos HTML puro (normalmente esto sería una PartialView)
        string htmlRespuesta = $@"
            <div class='bg-blue-100 border-l-4 border-blue-500 text-blue-700 p-4 mt-4' role='alert'>
                <p class='font-bold'>¡Respuesta del Servidor!</p>
                <p>Hora generada en backend: {DateTime.Now.ToString("HH:mm:ss")}</p>
            </div>";

        return Content(htmlRespuesta, "text/html");
    }
}
