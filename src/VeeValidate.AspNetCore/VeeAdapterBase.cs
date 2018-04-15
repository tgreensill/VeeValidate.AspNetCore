using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;

namespace VeeValidate.AspNetCore
{
    public abstract class VeeAdapterBase<TAttribute> : AttributeAdapterBase<TAttribute>
        where TAttribute : ValidationAttribute
    {
        private const string VeeValidateAttributeName = "v-validate";
        private readonly VeeValidateOptions _options;

        public VeeAdapterBase(TAttribute attribute, IStringLocalizer stringLocalizer, VeeValidateOptions options) : base(attribute, stringLocalizer)
        {
            _options = options;
        }

        protected bool MergeVeeValidateAttribute(ClientModelValidationContext context, string rule)
        {
            // Set display name in data-vv-as attribute for better validation messages
            MergeAttribute(context.Attributes, "data-vv-as", context.ModelMetadata.GetDisplayName());
            
            if (context.Attributes.TryGetValue(VeeValidateAttributeName, out var value))
            {
                context.Attributes.Add(VeeValidateAttributeName, "{" + $"{rule},{value.TrimStart('{').TrimEnd('}')}" + "}");
                return true;
            }
            
            context.Attributes.Add(VeeValidateAttributeName, "{" + rule + "}");
            return false;
        }

        protected string GetErrorMessage(ModelValidationContextBase validationContext, params object[] arguments)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException(nameof(validationContext));
            }

            return GetErrorMessage(
                validationContext.ModelMetadata,
                validationContext.ModelMetadata.GetDisplayName(),
                arguments);
        }
    }
}
