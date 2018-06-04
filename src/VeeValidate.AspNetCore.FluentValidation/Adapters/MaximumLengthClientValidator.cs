using FluentValidation.Internal;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.FluentValidation.Adapters
{
    public class MaximumLengthClientValidator : VeeValidateClientValidatorBase
    {
        public MaximumLengthClientValidator(PropertyRule rule, IPropertyValidator validator)
            : base(rule, validator)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var maxLengthValidator = (MaximumLengthValidator)Validator;
            
            MergeAttribute(context.Attributes, "data-vv-as", context.ModelMetadata.GetDisplayName());
            MergeValidationAttribute(context.Attributes, "max", maxLengthValidator.Max.ToString());
        }
    }
}
