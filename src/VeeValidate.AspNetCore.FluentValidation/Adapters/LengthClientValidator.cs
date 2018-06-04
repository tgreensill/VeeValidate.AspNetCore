using FluentValidation.Internal;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.FluentValidation.Adapters
{
    public class LengthClientValidator : VeeValidateClientValidatorBase
    {
        public LengthClientValidator(PropertyRule rule, IPropertyValidator validator)
            : base(rule, validator)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var lengthVal = (LengthValidator)Validator;

            MergeAttribute(context.Attributes, "data-vv-as", context.ModelMetadata.GetDisplayName());
            MergeValidationAttribute(context.Attributes, "max", lengthVal.Max.ToString());
            MergeValidationAttribute(context.Attributes, "min", lengthVal.Min.ToString());
        }
    }
}
