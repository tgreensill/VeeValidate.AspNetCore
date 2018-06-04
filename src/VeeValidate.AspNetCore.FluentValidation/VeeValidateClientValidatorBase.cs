using System.Collections.Generic;
using FluentValidation.AspNetCore;
using FluentValidation.Internal;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.FluentValidation
{
    public abstract class VeeValidateClientValidatorBase : ClientValidatorBase
    {
        private const string VeeValidateAttributeName = "v-validate";

        protected VeeValidateClientValidatorBase(PropertyRule rule, IPropertyValidator validator) : base(rule, validator)
        {
        }

        public abstract override void AddValidation(ClientModelValidationContext context);

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
