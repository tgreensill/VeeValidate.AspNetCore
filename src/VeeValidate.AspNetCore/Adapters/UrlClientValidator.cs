using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class UrlClientValidator : VeeAttributeAdapter<UrlAttribute>
    {
        public UrlClientValidator(UrlAttribute attribute) : base(attribute)
        {            
        }

        public override void AddValidationRules(ClientModelValidationContext context)
        {
            // AspNetCore modelstate validation requires the Url protocol to be included
            MergeValidationAttribute(context.Attributes, $"url:[true,true]");
        }
    }
}
