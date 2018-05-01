using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class StringLengthClientValidator : VeeAttributeClientValidator<StringLengthAttribute>
    {
        public StringLengthClientValidator(StringLengthAttribute attribute) : base(attribute)
        {
        }

        public override void AddValidationRules(ClientModelValidationContext context)
        {
            if (Attribute.MaximumLength != int.MaxValue)
            {
                MergeRule(context.Attributes, $"max:{Attribute.MaximumLength}");
            }

            if (Attribute.MinimumLength != 0)
            {
                MergeRule(context.Attributes, $"min:{Attribute.MinimumLength}");                
            }
        }
    }
}
