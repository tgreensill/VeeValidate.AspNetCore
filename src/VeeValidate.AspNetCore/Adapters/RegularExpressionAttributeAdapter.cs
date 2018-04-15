using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System;

namespace VeeValidate.AspNetCore.Adapters
{
    public class RegularExpressionAttributeAdapter : VeeAdapterBase<RegularExpressionAttribute>
    {
        public RegularExpressionAttributeAdapter(RegularExpressionAttribute attribute, IStringLocalizer stringLocalizer, VeeValidateOptions options) : base(attribute, stringLocalizer, options)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            MergeVeeValidateAttribute(context, $"regex:{Attribute.Pattern}");
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return GetErrorMessage(validationContext, Attribute.Pattern);            
        }
    }
}
