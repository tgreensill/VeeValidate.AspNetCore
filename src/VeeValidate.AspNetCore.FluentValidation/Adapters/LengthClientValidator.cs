using FluentValidation.AspNetCore;
using FluentValidation.Internal;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.FluentValidation.Adapters
{
    public class LengthClientValidator : ClientValidatorBase
    {
        public LengthClientValidator(PropertyRule rule, IPropertyValidator validator)
            : base(rule, validator)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var lengthVal = (LengthValidator)Validator;

            context
                .AddValidationDisplayName()
                .AddValidationRule("max", lengthVal.Max.ToString())
                .AddValidationRule("min", lengthVal.Min.ToString());
        }
    }
}
