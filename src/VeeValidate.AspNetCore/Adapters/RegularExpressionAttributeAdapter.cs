using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class RegularExpressionAttributeAdapter : IHtmlValidationAttributeAdapter
    {
        public string[] Attributes => new [] { "data-val-regex-pattern" };

        public void AddVeeValidateRules(string value, ModelMetadata metadata, IDictionary<string, string> rules)
        {
            rules.Merge("regex", $"/{value.Trim('/')}/");
        }
    }
}
