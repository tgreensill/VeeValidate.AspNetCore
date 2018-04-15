using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace VeeValidate.AspNetCore.Adapters
{
    public class CompareAttributeAdapter : VeeAdapterBase<CompareAttribute>
    {
        public CompareAttributeAdapter(CompareAttribute attribute, IStringLocalizer stringLocalizer, VeeValidateOptions options) : base(attribute, stringLocalizer, options)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            MergeVeeValidateAttribute(context, $"confirmed:{Attribute.OtherProperty}");
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return GetErrorMessage(validationContext, Attribute.OtherProperty);
        }
    }
}
