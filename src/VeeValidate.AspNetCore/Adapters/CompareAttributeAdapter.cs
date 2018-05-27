using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class CompareAttributeAdapter : IHtmlValidationAttributeAdapter
    {
        public string[] Attributes => new [] { "data-val-equalto-other" };

        public void AddVeeValidateRules(string value, ModelMetadata metadata, IDictionary<string, string> rules)
        {
            // Trim the leading "*." added for jquery and add single quotes to validate by field name 
            // rather than vue instance property.
            rules.Merge("confirmed", $"'{value.Replace("*.", "")}'");
        }
    }
}
