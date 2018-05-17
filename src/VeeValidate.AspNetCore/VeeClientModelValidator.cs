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

            AddValidationRules(context);
        }

        protected static void MergeValidationAttribute(IDictionary<string, string> attributes, string rule)
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
