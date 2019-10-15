using System.Reflection;
using FluentValidation.AspNetCore;
using FluentValidation.Internal;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.FluentValidation.Adapters
{
    public class EqualClientValidator : ClientValidatorBase
    {
        public EqualClientValidator(PropertyRule rule, IPropertyValidator validator)
            : base(rule, validator)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var equalValidator = (EqualValidator) Validator;
            
            // Only implementing for equals comparison.
            if (equalValidator.Comparison != Comparison.Equal)
            {
                return;
            }

            if (equalValidator.MemberToCompare != null)
            {
                // If propertyToCompare is not null then we're comparing to another property.
                // If propertyToCompare is null then we're either comparing against a literal value, a field or a method call.
                // We only care about property comparisons in this case.
                context.AddValidationRule("confirmed", $"'{equalValidator.MemberToCompare.Name}'");
            }
            else if (equalValidator.ValueToCompare != null)
            {                
                // Not implemented
            }
        }
    }
}
