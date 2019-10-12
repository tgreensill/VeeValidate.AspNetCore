using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class StringLengthAttributeAdapter : VeeValidateAttributeAdapter<StringLengthAttribute>
    {
        public StringLengthAttributeAdapter(StringLengthAttribute attribute) : base(attribute)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            context.AddValidationDisplayName();

            if (Attribute.MaximumLength != int.MaxValue)
            {
                context.AddValidationRule("max", Attribute.MaximumLength);
            }

            if (Attribute.MinimumLength != 0)
            {
                context.AddValidationRule("min", Attribute.MinimumLength);
            }
        }
    }
}
