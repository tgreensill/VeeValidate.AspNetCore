using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class RequiredClientValidator : VeeAttributeClientValidator<RequiredAttribute>
    {
        public RequiredClientValidator(RequiredAttribute attribute) : base(attribute)
        {
        }

        public override void AddValidationRules(ClientModelValidationContext context)
        {                        
            MergeRule(context.Attributes, "required:true");
        }
    }
}
