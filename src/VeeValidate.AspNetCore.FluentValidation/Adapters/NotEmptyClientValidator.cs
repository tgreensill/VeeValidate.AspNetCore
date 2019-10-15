using FluentValidation.AspNetCore;
using FluentValidation.Internal;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.FluentValidation.Adapters
{
    public class NotEmptyClientValidator : ClientValidatorBase
    {
        public NotEmptyClientValidator(PropertyRule rule, IPropertyValidator validator) : base(rule, validator)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            context.AddValidationRule("required", "true");

            // Add attribute to fire validation rules on both input and blur
            // so that the field becomes invalid when the user enters then leaves
            // without making any changes - immitate the jQuery validaiton behaviour.
            if (!context.Attributes.ContainsKey("data-vv-validate-on"))
            {
                context.Attributes.Add("data-vv-validate-on", "input|blur");
            }
        }
    }
}
