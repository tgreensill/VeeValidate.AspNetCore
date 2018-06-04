using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore
{
    public abstract class VeeValidateClientModelValidator : IClientModelValidator
    {
        private const string VeeValidateAttributeName = "v-validate";

        public abstract void AddValidation(ClientModelValidationContext context);
        
        protected bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }

            attributes.Add(key, value);
            return true;
        }

        protected void MergeValidationAttribute(IDictionary<string, string> attributes, string key, object value)
        {
            MergeValidationAttribute(attributes, key, value.ToString());
        }

        protected void MergeValidationAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            // Merge on v-validate field.
            if (attributes.TryGetValue(VeeValidateAttributeName, out var rules))
            {
                attributes[VeeValidateAttributeName] = $"{{{rules.TrimStart('{').TrimEnd('}')},{key}:{value}}}";
                return;
            }

            attributes.Add(VeeValidateAttributeName, $"{{{key}:{value}}}");
        }
    }
}
