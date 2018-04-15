using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;

namespace VeeValidate.AspNetCore.Adapters
{
    public class MinLengthAttributeAdapter : VeeAdapterBase<MinLengthAttribute>
    {
        public MinLengthAttributeAdapter(MinLengthAttribute attribute, IStringLocalizer stringLocalizer, VeeValidateOptions options) : base(attribute, stringLocalizer, options)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            MergeVeeValidateAttribute(context, $"min:{Attribute.Length}");
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return GetErrorMessage(validationContext, Attribute.Length);
        }
    }
}
