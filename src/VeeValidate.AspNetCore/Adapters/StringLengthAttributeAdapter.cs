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
            MergeAttribute(context.Attributes, "data-vv-as", context.ModelMetadata.GetDisplayName());

            if (Attribute.MaximumLength != int.MaxValue)
            {
                MergeValidationAttribute(context.Attributes, "max", Attribute.MaximumLength);
            }

            if (Attribute.MinimumLength != 0)
            {
                MergeValidationAttribute(context.Attributes, "min", Attribute.MinimumLength);
            }
        }
    }
}
