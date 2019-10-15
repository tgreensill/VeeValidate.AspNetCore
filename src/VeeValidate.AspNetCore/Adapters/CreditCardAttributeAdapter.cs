using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class CreditCardAttributeAdapter : VeeValidateAttributeAdapter<CreditCardAttribute>
    {
        public CreditCardAttributeAdapter(CreditCardAttribute attribute) : base(attribute)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            context.AddValidationRule("credit_card", "true");
        }
    }
}