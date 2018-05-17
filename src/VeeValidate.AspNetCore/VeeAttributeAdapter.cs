using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace VeeValidate.AspNetCore
{
    public abstract class VeeAttributeAdapter<TAttribute> : VeeClientModelValidator, IAttributeAdapter
        where TAttribute : ValidationAttribute
    {
        public TAttribute Attribute { get; set; }

        protected VeeAttributeAdapter(TAttribute attribute)
        {
            Attribute = attribute;
        }

        public string GetErrorMessage(ModelValidationContextBase validationContext)
        {            
            // Error messages should be handled by vee-validate... I think.
            throw new ArgumentNullException(nameof(validationContext));            
        }
    }
}
