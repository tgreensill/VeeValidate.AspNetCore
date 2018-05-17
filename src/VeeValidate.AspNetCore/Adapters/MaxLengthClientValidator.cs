using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class MaxLengthClientValidator : VeeAttributeAdapter<MaxLengthAttribute>
    {
        public MaxLengthClientValidator(MaxLengthAttribute attribute) : base(attribute)
        {
        }

        public override void AddValidationRules(ClientModelValidationContext context)
        {
            MergeValidationAttribute(context.Attributes, $"max:{Attribute.Length}");
        }
    }
}
