using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class EmailAddressAttributeAdapter : IHtmlValidationAttributeAdapter
    {
        public string[] Attributes => new [] { "data-val-email" };

        public void AddVeeValidateRules(string value, ModelMetadata metadata, IDictionary<string, string> rules)
        {
            rules.Merge("email", "true");
        }
    }
}
