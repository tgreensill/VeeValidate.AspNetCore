using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using VeeValidate.AspNetCore.ViewFeatures;

namespace VeeValidate.AspNetCore
{
    public abstract class VeeValidateClientModelValidator : IClientModelValidator
    {
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
            VueHtmlAttributeHelper.MergeVeeValidateAttribute(attributes, key, value);
        }
    }
}
