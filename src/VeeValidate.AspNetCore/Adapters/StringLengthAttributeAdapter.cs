using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace VeeValidate.AspNetCore.Adapters
{
    public class StringLengthAttributeAdapter : VeeAdapterBase<StringLengthAttribute>
    {
        public StringLengthAttributeAdapter(StringLengthAttribute attribute, IStringLocalizer stringLocalizer, VeeValidateOptions options) : base(attribute, stringLocalizer, options)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (Attribute.MaximumLength != int.MaxValue)
            {
                MergeVeeValidateAttribute(context, $"max:{Attribute.MaximumLength}");
            }

            if (Attribute.MinimumLength != 0)
            {
                MergeVeeValidateAttribute(context, $"min:{Attribute.MinimumLength}");                
            }
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return GetErrorMessage(
                validationContext,                
                Attribute.MaximumLength,
                Attribute.MinimumLength);
        }
    }
}
