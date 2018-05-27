using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class MaxLengthAttributeAdapter : IHtmlValidationAttributeAdapter
    {
        public string[] Attributes => new []
        {
            "data-val-maxlength-max", "data-val-length-max"
        };

        public void AddVeeValidateRules(string value, ModelMetadata metadata, IDictionary<string, string> rules)
        {
            rules.Merge("max", value);
        }
    }
}
