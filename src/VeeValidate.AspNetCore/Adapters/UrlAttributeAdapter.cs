using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class UrlAttributeAdapter : IHtmlValidationAttributeAdapter
    {
        public string[] Attributes => new [] { "data-val-url" };

        public void AddVeeValidateRules(string value, ModelMetadata metadata, IDictionary<string, string> rules)
        {
            rules.Merge("url", "[true,true]");
        }
    }
}
