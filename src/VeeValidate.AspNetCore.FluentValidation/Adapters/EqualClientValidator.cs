using System.Reflection;
using FluentValidation.Internal;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.FluentValidation.Adapters
{
    public class EqualClientValidator : VeeValidateClientValidatorBase
    {
        public EqualClientValidator(PropertyRule rule, IPropertyValidator validator)
            : base(rule, validator)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var propertyToCompare = ((EqualValidator)Validator).MemberToCompare as PropertyInfo;

            if (propertyToCompare != null)
            {
                // If propertyToCompare is not null then we're comparing to another property.
                // If propertyToCompare is null then we're either comparing against a literal value, a field or a method call.
                // We only care about property comparisons in this case.
                MergeAttribute(context.Attributes, "data-vv-as", context.ModelMetadata.GetDisplayName());
                MergeValidationAttribute(context.Attributes, "confirmed", $"'{propertyToCompare.Name}'");
            }
        }
    }
}
