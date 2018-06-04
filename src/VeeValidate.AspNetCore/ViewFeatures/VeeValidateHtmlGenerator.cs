using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.Extensions.Options;

namespace VeeValidate.AspNetCore.ViewFeatures
{
    public class VeeValidateHtmlGenerator : DefaultHtmlGenerator
    {
        private readonly VeeValidateOptions _options;

        public VeeValidateHtmlGenerator(IAntiforgery antiforgery, IOptions<MvcViewOptions> optionsAccessor, IModelMetadataProvider metadataProvider, IUrlHelperFactory urlHelperFactory, HtmlEncoder htmlEncoder, ValidationHtmlAttributeProvider validationAttributeProvider, VeeValidateOptions options)
            : base(antiforgery, optionsAccessor, metadataProvider, urlHelperFactory, htmlEncoder, validationAttributeProvider)        
        {
            _options = options;
        }

        public override TagBuilder GenerateValidationSummary(
            ViewContext viewContext, 
            bool excludePropertyErrors, 
            string message, 
            string headerTag, 
            object htmlAttributes)
        {
            if (viewContext == null)
            {
                throw new ArgumentNullException(nameof(viewContext));
            }

            var tagBuilder = new TagBuilder("div");
            tagBuilder.MergeAttributes(GetHtmlAttributeDictionaryOrNull(htmlAttributes));
            tagBuilder.AddCssClass(_options.ValidationSummaryCssClassName);

            // The v-cloak attribute is removed once the vue instance has been initialized on the page.
            // It's useful for hiding elements whose visibility is controller by the vue instance until it's ready. 
            tagBuilder.MergeAttribute("v-cloak", null);

            if (excludePropertyErrors)
            {
                tagBuilder.MergeAttribute("v-show", "validationSummaryErrors && validationSummaryErrors.length > 0");
                tagBuilder.InnerHtml.SetHtmlContent(new HtmlString("<ul><li v-for=\"error in validationSummaryErrors\">{{error}}</li></ul>"));
            }
            else
            {
                // The validation summary will only appear when there's an error in the error bag.
                tagBuilder.MergeAttribute("v-show", $"{_options.ErrorBagName}.any()");
                tagBuilder.InnerHtml.SetHtmlContent(new HtmlString($"<ul><li v-for=\"error in {_options.ErrorBagName}.all()\">{{{{error}}}}</li></ul>"));
            }

            return tagBuilder;
        }

        public override TagBuilder GenerateValidationMessage(
           ViewContext viewContext,
           ModelExplorer modelExplorer,
           string expression,
           string message,
           string tag,
           object htmlAttributes)
        {
            if (viewContext == null)
            {
                throw new ArgumentNullException(nameof(viewContext));
            }

            if (string.IsNullOrEmpty(tag))
            {
                tag = viewContext.ValidationMessageElement;
            }

            var fullName = NameAndIdProvider.GetFullHtmlFieldName(viewContext, expression);
            var htmlAttributeDictionary = GetHtmlAttributeDictionaryOrNull(htmlAttributes);

            var tagBuilder = new TagBuilder(tag);
            tagBuilder.MergeAttributes(htmlAttributeDictionary);
            tagBuilder.AddCssClass(_options.ValidationMessageCssClassName);

            // The v-cloak attribute is removed once the vue instance has been initialized on the page.
            // It's useful for hiding elements whose visibility is controller by the vue instance until it's ready. 
            tagBuilder.MergeAttribute("v-cloak", null);
            // The span will only appear when there's an error in the error bag for the field .           
            tagBuilder.MergeAttribute("v-show",  $"{_options.ErrorBagName}.has('{fullName}')");
            tagBuilder.InnerHtml.SetHtmlContent(new HtmlString($"{{{{{_options.ErrorBagName}.first('{fullName}')}}}}"));
            
            return tagBuilder;
        }
        
        private static IDictionary<string, object> GetHtmlAttributeDictionaryOrNull(object htmlAttributes)
        {
            IDictionary<string, object> htmlAttributeDictionary = null;
            if (htmlAttributes != null)
            {
                htmlAttributeDictionary = htmlAttributes as IDictionary<string, object>;
                if (htmlAttributeDictionary == null)
                {
                    htmlAttributeDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                }
            }

            return htmlAttributeDictionary;
        }
    }
}
