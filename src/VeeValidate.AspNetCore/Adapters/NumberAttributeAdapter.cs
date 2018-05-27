using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class NumberAttributeAdapter : IHtmlValidationAttributeAdapter, IHtmlInputTypeAttributeAdapter
    {
        public string[] Attributes => new[] { "data-val-number" };

        public string[] InputTypes => new[] { "number" };

        public void AddVeeValidateRules(string value, ModelMetadata metadata, IDictionary<string, string> rules)
        {
            var typeToValidate = metadata.UnderlyingOrModelType;

            if (typeToValidate == typeof(float) ||
                typeToValidate == typeof(double) ||
                typeToValidate == typeof(decimal))
            {
                rules.Merge("decimal", "true");
                return;
            }

            rules.Merge("numeric", "true");
        }
    }
}
