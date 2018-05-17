using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class RequiredClientValidator : VeeAttributeAdapter<RequiredAttribute>
    {
        public RequiredClientValidator(RequiredAttribute attribute) : base(attribute)
        {
        }

        public override void AddValidationRules(ClientModelValidationContext context)
        {                        
            MergeValidationAttribute(context.Attributes, "required:true");
        }
    }
}
