using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class MinLengthAttributeAdapter : IHtmlValidationAttributeAdapter
    {
        public string[] Attributes => new []
        {
            "data-val-minlength-min", "data-val-length-min" 
        };

        public void AddVeeValidateRules(string value, ModelMetadata metadata, IDictionary<string, string> rules)
        {
            rules.Merge("min", value);
        }
    }
}
