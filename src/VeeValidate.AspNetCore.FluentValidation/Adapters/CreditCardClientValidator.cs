using FluentValidation.Internal;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.FluentValidation.Adapters
{
    public class CreditCardClientValidator : VeeValidateClientValidatorBase
    {
        public CreditCardClientValidator(PropertyRule rule, IPropertyValidator validator)
            : base(rule, validator)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-vv-as", context.ModelMetadata.GetDisplayName());
            MergeValidationAttribute(context.Attributes, "credit_card", "true");
        }
    }
}
