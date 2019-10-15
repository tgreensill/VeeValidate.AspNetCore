using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class RegularExpressionAttributeAdapter : VeeValidateAttributeAdapter<RegularExpressionAttribute>
    {
        public RegularExpressionAttributeAdapter(RegularExpressionAttribute attribute) : base(attribute)
        {
        }
        
        public override void AddValidation(ClientModelValidationContext context)
        {
            // Ensure the pattern starts and ends with '/'
            context.AddValidationRule("regex", $"/{Attribute.Pattern.Trim('/')}/");
        }
    }
}
