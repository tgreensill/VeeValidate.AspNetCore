using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class RequiredAttributeAdapter : VeeValidateAttributeAdapter<RequiredAttribute>
    {
        public RequiredAttributeAdapter(RequiredAttribute attribute) : base(attribute)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            context
                .AddValidationDisplayName()
                .AddValidationRule("required", "true");
        }
    }
}
