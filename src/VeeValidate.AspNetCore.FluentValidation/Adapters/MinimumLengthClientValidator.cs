using FluentValidation.AspNetCore;
using FluentValidation.Internal;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.FluentValidation.Adapters
{
    public class MinimumLengthClientValidator : ClientValidatorBase
    {
        public MinimumLengthClientValidator(PropertyRule rule, IPropertyValidator validator)
            : base(rule, validator)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var minLengthValidator = (MinimumLengthValidator)Validator;

            context
                .AddValidationDisplayName()
                .AddValidationRule("min", minLengthValidator.Min.ToString());
        }
    }
}
