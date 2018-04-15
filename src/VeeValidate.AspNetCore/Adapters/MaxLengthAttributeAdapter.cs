using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace VeeValidate.AspNetCore.Adapters
{
    public class MaxLengthAttributeAdapter : VeeAdapterBase<MaxLengthAttribute>
    {
        public MaxLengthAttributeAdapter(MaxLengthAttribute attribute, IStringLocalizer stringLocalizer, VeeValidateOptions options) : base(attribute, stringLocalizer, options)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            MergeVeeValidateAttribute(context, $"max:{Attribute.Length}");            
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return GetErrorMessage(validationContext, Attribute.Length);
        }
    }
}
