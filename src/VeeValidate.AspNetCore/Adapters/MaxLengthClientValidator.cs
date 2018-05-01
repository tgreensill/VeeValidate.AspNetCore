using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class MaxLengthClientValidator : VeeAttributeClientValidator<MaxLengthAttribute>
    {
        public MaxLengthClientValidator(MaxLengthAttribute attribute) : base(attribute)
        {
        }

        public override void AddValidationRules(ClientModelValidationContext context)
        {
            MergeRule(context.Attributes, $"max:{Attribute.Length}");
        }
    }
}
