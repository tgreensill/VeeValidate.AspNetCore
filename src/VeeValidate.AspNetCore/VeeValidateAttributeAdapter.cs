using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore
{
    public abstract class VeeValidateAttributeAdapter<TAttribute> : IClientModelValidator, IAttributeAdapter
        where TAttribute : ValidationAttribute
    {
        protected TAttribute Attribute { get; private set; }

        protected VeeValidateAttributeAdapter(TAttribute attribute)
        {
            Attribute = attribute;
        }

        public abstract void AddValidation(ClientModelValidationContext context);

        [Obsolete("The client side messages should be handled by VeeValidate")]
        public string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            // This should not get hit.
            throw new Exception();
        }
    }
}
