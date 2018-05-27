using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class CreditCardAttributeAdapter : IHtmlValidationAttributeAdapter
    {
        public string[] Attributes => new [] { "data-val-creditcard" };

        public void AddVeeValidateRules(string value, ModelMetadata metadata, IDictionary<string, string> rules)
        {
            rules.Merge("credit_card", "true");            
        }
    }
}