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
    public class VeeValidationHtmlGenerator : DefaultHtmlGenerator
    {
        private readonly VeeValidateOptions _options;

        public VeeValidationHtmlGenerator(IAntiforgery antiforgery, IOptions<MvcViewOptions> optionsAccessor, IModelMetadataProvider metadataProvider, IUrlHelperFactory urlHelperFactory, HtmlEncoder htmlEncoder, ValidationHtmlAttributeProvider validationAttributeProvider, VeeValidateOptions options)
            : base(antiforgery, optionsAccessor, metadataProvider, urlHelperFactory, htmlEncoder, validationAttributeProvider)        
        {
            _options = options;
        }

        //TODO - public override TagBuilder GenerateValidationSummary()

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

            // Only the style of the span is changed according to the errors if message is null or empty.
            // Otherwise the content and style is handled by the client-side validation.
            tagBuilder.AddCssClass(_options.ValidationMessageCssClassName); 
            tagBuilder.MergeAttribute("v-show", $"{_options.ErrorBagName}.has('{fullName}')");
            tagBuilder.InnerHtml.SetHtmlContent(new HtmlString($"{{{{{_options.ErrorBagName}.first('{fullName}')}}}}"));
            
            return tagBuilder;
        }

        // Only need a dictionary if htmlAttributes is non-null. TagBuilder.MergeAttributes() is fine with null.
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
