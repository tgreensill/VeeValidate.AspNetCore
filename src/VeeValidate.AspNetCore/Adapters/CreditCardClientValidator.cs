using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class CreditCardClientValidator : VeeAttributeAdapter<CreditCardAttribute>
    {
        public CreditCardClientValidator(CreditCardAttribute attribute) : base(attribute)
        {
        }

        public override void AddValidationRules(ClientModelValidationContext context)
        {
            MergeRule(context.Attributes, "credit_card:true");
        }
    }
}
