using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class EmailAddressAttributeAdapter : VeeValidateAttributeAdapter<EmailAddressAttribute>
    {
        public EmailAddressAttributeAdapter(EmailAddressAttribute attribute) : base(attribute)
        {
        }
        
        public override void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-vv-as", context.ModelMetadata.GetDisplayName());
            MergeValidationAttribute(context.Attributes, "email", "true");
        }
    }
}
