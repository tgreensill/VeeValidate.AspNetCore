using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class RegularExpressionClientValidator : VeeAttributeAdapter<RegularExpressionAttribute>
    {
        public RegularExpressionClientValidator(RegularExpressionAttribute attribute) : base(attribute)
        {
        }

        public override void AddValidationRules(ClientModelValidationContext context)
        {
            // TODO - Add / to the start and / to the end when appropriate
            MergeValidationAttribute(context.Attributes, $"regex:{Attribute.Pattern}");
        }
    }
}
