using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class DateAttributeAdapter : IHtmlValidationAttributeAdapter, IHtmlInputTypeAttributeAdapter
    {
        private readonly VeeValidateOptions _options;
        
        public DateAttributeAdapter(VeeValidateOptions options)
        {
            _options = options;
        }

        public string[] Attributes => new[] { "data-val-date" };

        public string[] InputTypes => new[] { "date", "datetime-local" };

        public void AddVeeValidateRules(string value, ModelMetadata metadata, IDictionary<string, string> rules)
        {
            rules.Merge("date_format", $"'{_options.Dates.Format}'");
        }

        public string GetVeeValidateRule(string value, ModelMetadata metadata)
        {
            return $"date_format:";            
        }
    }
}
