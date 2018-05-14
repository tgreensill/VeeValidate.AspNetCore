using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class EmailAddressClientValidator : VeeAttributeAdapter<EmailAddressAttribute>
    {
        public EmailAddressClientValidator(EmailAddressAttribute attribute) : base(attribute)
        {
        }

        public override void AddValidationRules(ClientModelValidationContext context)
        {
            MergeRule(context.Attributes, "email:true");
        }
    }
}
