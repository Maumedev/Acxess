using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Acxess.Catalog.Application.Features.AccessTiers.Queries.GetAccessTiers;
using Acxess.Catalog.Application.Features.SellingPlans.Commands.NewSellingPlan;
using Acxess.Catalog.Application.Features.SellingPlans.Queries.GetSellingPlans;
using Acxess.Catalog.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Acxess.Web.Pages.Catalog.SellingPlans;

public class IndexModel(IMediator mediator) : PageModel
{
    public List<AccessTierDto> accessTiers = new List<AccessTierDto>
    {
        new(1, "Acceso General", "", true),
         new(2, "Spa", "", true),
         new(3, "Albercas", "", true)
    };

    public List<SellingPlanDto> items = new List<SellingPlanDto>();

    [BindProperty]
    public SellingPlanInputModel Input {get; set;} = new();
    public async Task OnGet()
    {
        var resultSellingPLans = await mediator.Send(new GetSellingPlanQuery(true));

        if (resultSellingPLans.IsSuccess)
        {
            items = resultSellingPLans.Value;
        }
    }

    public async Task<IActionResult> OnPostSaveAsync()
    {
        // ModelState.AddModelError(string.Empty, "prueba de errores");
        if (!ModelState.IsValid)
        {

            // var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            // foreach(var err in errors) Console.WriteLine($"ERROR BACKEND: {err}");

            // ViewData.TemplateInfo.HtmlFieldPrefix = "Input";
            return Partial("_Form", Input); 
        }

        var userNumberString = User.FindFirstValue("UserNumber");

        int userNumber = int.TryParse(userNumberString, out var val) ? val : 0;

        if (userNumber == 0)  return Unauthorized(); 

        var command =  new NewSellingPlanCommand(
            Input.TotalMembers,
            Input.DurationInValue,
            (DurationUnit)Input.DurationUnit,
            Input.Name,
            Input.Price,
            userNumber,
            Input.AccessTiersIds
        );

        var resultSaved = await mediator.Send(command);

        if (resultSaved.IsFailure)
        {
            ModelState.AddModelError(string.Empty, resultSaved.Error.Description);
            return Partial("_Form", Input); 
        }

        var resultSellingPLans = await mediator.Send(new GetSellingPlanQuery(true));
        if (resultSellingPLans.IsSuccess)
        {
            items = resultSellingPLans.Value;
        }

        var triggers = new
        {
            listUpdated = items, 
            notify = new { message = resultSaved.Value, type = "success" }
        };
        
        Response.Headers.Append("HX-Trigger", JsonSerializer.Serialize(triggers)); 
        return new NoContentResult(); 
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await Task.Delay(1000);
        Console.WriteLine("eliminado");
        return new NoContentResult();
    }
}

