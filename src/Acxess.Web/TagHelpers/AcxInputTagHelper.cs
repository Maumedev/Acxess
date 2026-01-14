using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Acxess.Web.TagHelpers;

[HtmlTargetElement("acx-input", Attributes = "asp-for")]
public class AcxInputTagHelper : TagHelper
{
    public ModelExpression AspFor { get; set; } = null!;

    public string Label { get; set; } = string.Empty;
    public string? Placeholder { get; set; }
    public string? Icon { get; set; }


    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Attributes.SetAttribute("class", "mb-4");
        
        // 1. Label
        if (!string.IsNullOrEmpty(Label))
        {
            output.Content.AppendHtml($@"
                <label class='block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1' for='{AspFor.Name}'>
                    {Label}
                </label>");
        }
        
        output.Content.AppendHtml("<div class='relative group'>");
        
        var paddingLeft = "p-2.5";
        if (!string.IsNullOrEmpty(Icon))
        {
            paddingLeft = "p-2.5 pl-7";
            output.Content.AppendHtml($@"
                <span class='absolute left-3 top-2.5 text-gray-400 group-focus-within:text-blue-500 transition-colors pointer-events-none'>
                    {Icon}
                </span>");
        }
        
        var inputClasses = $"w-full {paddingLeft} bg-white dark:bg-slate-900 border border-gray-300 dark:border-slate-600 rounded-lg focus:ring-2 focus:ring-blue-500 outline-none shadow-sm text-gray-900 dark:text-white transition-colors disabled:bg-gray-100 disabled:text-gray-500";
        var inputBuilder = new TagBuilder("input");
        inputBuilder.Attributes.Add("id", AspFor.Name);
        inputBuilder.Attributes.Add("name", AspFor.Name);
        inputBuilder.Attributes.Add("class", inputClasses);
        
        if (AspFor.Model != null) inputBuilder.Attributes.Add("value", AspFor.Model.ToString());
        
        foreach (var attribute in context.AllAttributes)
        {
            if (!attribute.Name.StartsWith("asp-") && attribute.Name != "label" && attribute.Name != "icon")
            {
                inputBuilder.Attributes.Add(attribute.Name, attribute.Value.ToString());
            }
        }
        
        output.Content.AppendHtml(inputBuilder);
        output.Content.AppendHtml("</div>"); // Cierre container

        // 3. Validation
        output.Content.AppendHtml($@"<span class='text-xs text-red-500 font-bold mt-1 block' data-valmsg-for='{AspFor.Name}' data-valmsg-replace='true'></span>");
    }
}