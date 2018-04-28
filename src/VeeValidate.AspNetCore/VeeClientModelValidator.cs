using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace VeeValidate.AspNetCore
{
    public abstract class VeeClientModelValidator : IClientModelValidator
    {
        private const string VeeValidateAttributeName = "v-validate";

        public abstract void AddValidationRules(ClientModelValidationContext context);

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // TODO - User aliases rather than display name when localization is enabled
            // Set display name in data-vv-as attribute for better validation messages
            MergeAttribute(context.Attributes, "data-vv-as", context.ModelMetadata.GetDisplayName());

            AddValidationRules(context);
        }

        protected bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }

            attributes.Add(key, value);
            return true;
        }

        protected static void MergeRule(IDictionary<string, string> attributes, string rule)
        {
            if (attributes.TryGetValue(VeeValidateAttributeName, out var value))
            {
                attributes[VeeValidateAttributeName] = "{" + $"{value.TrimStart('{').TrimEnd('}')},{rule}" + "}";
                return;
            }

            attributes.Add(VeeValidateAttributeName, "{" + rule + "}");
        }        
    }
}
