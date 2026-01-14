using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Acxess.Web.TagHelpers;

[HtmlTargetElement("acx-switch", Attributes = "asp-for")]
public class AcxSwitchTagHelper : TagHelper
{
    public ModelExpression AspFor { get; set; } = null!;
    public string LabelOn { get; set; } = "Activo";
    public string LabelOff { get; set; } = "Inactivo";

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "label";
        output.Attributes.SetAttribute("class", "flex items-center cursor-pointer group select-none");

        var isChecked = (bool)(AspFor.Model ?? false);
        var xModel = context.AllAttributes["x-model"]?.Value.ToString(); 
        var condition = !string.IsNullOrEmpty(xModel) ? xModel : isChecked.ToString().ToLower();

        output.Content.AppendHtml($@"
            <div class='relative'>
                <input type='checkbox' id='{AspFor.Name}' name='{AspFor.Name}' value='true' class='sr-only' {(isChecked ? "checked" : "")} 
                       {(string.IsNullOrEmpty(xModel) ? "" : $"x-model='{xModel}'")} />
                
                <div :class=""{condition} ? 'bg-green-500' : 'bg-gray-300 dark:bg-slate-600'"" 
                     class='w-10 h-6 rounded-full shadow-inner transition-colors'></div>
                
                <div :class=""{condition} ? 'translate-x-4' : 'translate-x-0'"" 
                     class='absolute left-1 top-1 bg-white w-4 h-4 rounded-full shadow transition-transform transform'></div>
            </div>
            <span class='ml-3 text-sm font-medium text-gray-700 dark:text-gray-300 group-hover:text-blue-500 transition-colors' 
                  x-text=""{condition} ? '{LabelOn}' : '{LabelOff}'"">
            </span>
        ");
        
    }
}