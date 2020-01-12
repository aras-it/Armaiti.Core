using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace Armaiti.Core.TagHelpers
{
    /// <summary>
    /// This class creates a bootstrap dropdown menu for available languages in supported cultures of <see cref="RequestLocalizationOptions"/>.
    /// </summary>
    [HtmlTargetElement("languages", TagStructure = TagStructure.WithoutEndTag)]
    public class LanguagesTagHelper : TagHelper
    {
        private readonly IOptions<RequestLocalizationOptions> _locOptions;

        public LanguagesTagHelper(IOptions<RequestLocalizationOptions> locOptions)
        {
            _locOptions = locOptions;
        }

        /// <summary>
        /// Caption of dropdown menu.
        /// </summary>
        [HtmlAttributeName("caption")]
        public string Caption { get; set; } = "Languages";

        /// <summary>
        /// Css class for dropdown toggle button.
        /// </summary>
        [HtmlAttributeName("class")]
        public string Class { get; set; } = "btn-light";

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";
            output.Attributes.Add("class", "navbar-nav px-1");
            var returnUrl = string.IsNullOrEmpty(ViewContext.HttpContext.Request.Path)
                    ? "~/"
                    : $"~{ViewContext.HttpContext.Request.Path.Value}";
            output.Content.SetHtmlContent(GetCotentTemplate(returnUrl));
            output.TagMode = TagMode.StartTagAndEndTag;
        }

        private string GetCotentTemplate(string returnUrl)
        {
            var content = "<li class=\"nav-item\">" +
                $"<form class=\"form-horizontal\" method=\"post\" role=\"form\">" +
                "<div class=\"dropdown\">" +
                $"<button type=\"button\" class=\"btn dropdown-toggle {Class}\" data-toggle=\"dropdown\">" +
                $"<span{(CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft ? " class=\"ml-1\"" : "")}>{Caption}</span></button>" +
                $"<div class=\"dropdown-menu\" style=\"text-align:{(CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft ? "right" : "left")};\">";

            foreach (var culture in _locOptions.Value.SupportedCultures)
            {
                content += $"<button type=\"submit\" dir=\"{(culture.TextInfo.IsRightToLeft ? "rtl" : "ltr")}\" " +
                    "class=\"dropdown-item nav-link btn btn-link small text-muted\" " +
                    $"formaction=\"/Language/Set/?lcid={culture.LCID}&returnurl={returnUrl}\">{culture.NativeName}</button>";
            }

            content += "</div></form></li>";

            return content;
        }
    }
}
