using FluentValidation.Internal;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.FluentValidation.Adapters
{
    public class RegularExpressionClientValidator : VeeValidateClientValidatorBase
    {
        public RegularExpressionClientValidator(PropertyRule rule, IPropertyValidator validator)
            : base(rule, validator)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var regexVal = (RegularExpressionValidator)Validator;

            MergeAttribute(context.Attributes, "data-vv-as", context.ModelMetadata.GetDisplayName());
            // Ensure the pattern starts and ends with '/'
            MergeValidationAttribute(context.Attributes, "regex", $"/{regexVal.Expression.Trim('/')}/");
        }
    }
}
