using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class RequiredAttributeAdapter : IHtmlValidationAttributeAdapter
    {
        public string[] Attributes => new [] { "data-val-required" };

        public void AddVeeValidateRules(string value, ModelMetadata metadata, IDictionary<string, string> rules)
        {
            rules.Merge("required", "true");
        }
    }
}
