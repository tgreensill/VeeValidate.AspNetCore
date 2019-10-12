using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class MinLengthAttributeAdapter : VeeValidateAttributeAdapter<MinLengthAttribute>
    {
        public MinLengthAttributeAdapter(MinLengthAttribute attribute) : base(attribute)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            context
                .AddValidationDisplayName()
                .AddValidationRule("min", Attribute.Length);
        }
    }
}
