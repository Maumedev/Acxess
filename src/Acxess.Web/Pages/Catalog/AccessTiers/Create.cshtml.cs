using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Acxess.Web.Pages.Catalog.AccessTiers;

public class CreateSellingPlanInput
{
    [Required(ErrorMessage = "El nombre es obligatorio. ")]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
public class CreateModel : PageModel
{

    [BindProperty]
    public CreateSellingPlanInput Input { get; set; } = new();

    public IActionResult OnGet()
    {
        if (Request.Headers.ContainsKey("HX-Request") || Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return Partial("_Create", this);
        }
        
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        await Task.Delay(5000);
        // if (!ModelState.IsValid) 
        // {
        //     return Partial("_Create", this);
        // }
        // bool huboErrorAlGuardar = true;

        // if (huboErrorAlGuardar)
        // {
        //     ModelState.AddModelError(string.Empty, "Error: No se ha implementado la persistencia aún.");
        //     return Partial("_Create", this);
        // }

        // Aquí iría la lógica para guardar el nuevo nivel de acceso en la base de datos
        // var triggers = new { 
        //     closeSlide = true,      
        //     refreshTable = true 
        // };

        var triggers = new { 
            closeSlide = true,      
            refreshTable = true,
            notify = new { 
                type = "error", 
                message = "Nivel de acceso actualizado correctamente." 
            }
        };


        Response.Headers.Append("HX-Trigger", JsonSerializer.Serialize(triggers));
        return new NoContentResult();
    }


}
