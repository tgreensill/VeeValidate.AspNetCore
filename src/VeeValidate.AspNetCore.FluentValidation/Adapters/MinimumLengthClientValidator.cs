using FluentValidation.Internal;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.FluentValidation.Adapters
{
    public class MinimumLengthClientValidator : VeeValidateClientValidatorBase
    {
        public MinimumLengthClientValidator(PropertyRule rule, IPropertyValidator validator)
            : base(rule, validator)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var minLengthValidator = (MinimumLengthValidator)Validator;

            MergeAttribute(context.Attributes, "data-vv-as", context.ModelMetadata.GetDisplayName());
            MergeValidationAttribute(context.Attributes, "min", minLengthValidator.Min.ToString());
        }
    }
}
