using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace VeeValidate.AspNetCore.Adapters
{
    public class MinLengthClientValidator : VeeAttributeClientValidator<MinLengthAttribute>
    {
        public MinLengthClientValidator(MinLengthAttribute attribute) : base(attribute)
        {
        }
                
        public override void AddValidationRules(ClientModelValidationContext context)
        {
            MergeRule(context.Attributes, $"min:{Attribute.Length}");
        }
    }
}
