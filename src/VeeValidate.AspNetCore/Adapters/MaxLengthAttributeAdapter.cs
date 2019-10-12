using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class MaxLengthAttributeAdapter : VeeValidateAttributeAdapter<MaxLengthAttribute>
    {
        public MaxLengthAttributeAdapter(MaxLengthAttribute attribute) : base(attribute)
        {
        }
        
        public override void AddValidation(ClientModelValidationContext context)
        {
            context
                .AddValidationDisplayName()
                .AddValidationRule("max", Attribute.Length);
        }
    }
}
