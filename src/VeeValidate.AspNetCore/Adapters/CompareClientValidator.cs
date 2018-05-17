using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace VeeValidate.AspNetCore.Adapters
{
    public class CompareClientValidator : VeeAttributeAdapter<CompareAttribute>
    {
        public CompareClientValidator(CompareAttribute attribute) : base(attribute)
        {
        }

        public override void AddValidationRules(ClientModelValidationContext context)
        {
            // TODO - Find a way to get the full name
            var otherPropertyFullName = NameAndIdProvider.GetFullHtmlFieldName((ViewContext)context.ActionContext, Attribute.OtherProperty);
            otherPropertyFullName = ((ViewContext)context.ActionContext).ViewData.TemplateInfo.GetFullHtmlFieldName(Attribute.OtherProperty);
            MergeValidationAttribute(context.Attributes, $"confirmed:{otherPropertyFullName}");
        }
    }
}
