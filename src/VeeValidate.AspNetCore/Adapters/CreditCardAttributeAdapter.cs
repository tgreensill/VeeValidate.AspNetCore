using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class CreditCardAttributeAdapter : IHtmlValidationAttributeAdapter
    {
        public string[] Keys => new [] { "data-val-creditcard" };

        public string GetVeeValidateRule(string value, ModelMetadata metadata)
        {
            return "credit_card:true";
        }
    }
}