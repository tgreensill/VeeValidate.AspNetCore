using FluentValidation.AspNetCore;
using FluentValidation.Internal;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.FluentValidation.Adapters
{
    public class MaximumLengthClientValidator : ClientValidatorBase
    {
        public MaximumLengthClientValidator(PropertyRule rule, IPropertyValidator validator)
            : base(rule, validator)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var maxLengthValidator = (MaximumLengthValidator)Validator;
            
            context
                .AddValidationDisplayName()
                .AddValidationRule("max", maxLengthValidator.Max.ToString());
        }
    }
}
