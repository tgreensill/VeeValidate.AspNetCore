using FluentValidation.AspNetCore;
using FluentValidation.Internal;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.FluentValidation.Adapters
{
    public class RegularExpressionClientValidator : ClientValidatorBase
    {
        public RegularExpressionClientValidator(PropertyRule rule, IPropertyValidator validator)
            : base(rule, validator)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var regexVal = (RegularExpressionValidator)Validator;

            // Ensure the pattern starts and ends with '/'
            context.AddValidationRule("regex", $"/{regexVal.Expression.Trim('/')}/");
        }
    }
}
