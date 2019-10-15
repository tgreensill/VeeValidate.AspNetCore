using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class UrlAttributeAdapter : VeeValidateAttributeAdapter<UrlAttribute>
    {
        public UrlAttributeAdapter(UrlAttribute attribute) : base(attribute)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            context.AddValidationRule("url", "[true,true]");
        }
    }
}
