using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace VeeValidate.AspNetCore.Adapters
{
    public class RequiredAttributeAdapter : VeeAdapterBase<RequiredAttribute>
    {
        public RequiredAttributeAdapter(RequiredAttribute attribute, IStringLocalizer stringLocalizer, VeeValidateOptions options) : base(attribute, stringLocalizer, options)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
                        
            MergeVeeValidateAttribute(context, "required:true");
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException(nameof(validationContext));
            }

            return GetErrorMessage(validationContext.ModelMetadata, validationContext.ModelMetadata.GetDisplayName());
        }
    }
}
