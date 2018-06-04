using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using VeeValidate.AspNetCore.Adapters;

namespace VeeValidate.AspNetCore
{
    public class VeeValidateAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly VeeValidateOptions _options;
        
        public VeeValidateAttributeAdapterProvider(VeeValidateOptions options)
        {
            _options = options;
        }

        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {

            if (attribute == null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            if (_options == null)
            {
                throw new ArgumentNullException(nameof(_options));
            }

            IAttributeAdapter adapter;

            if (attribute is CompareAttribute compareAttribute)
            {
                adapter = new CompareClientValidator(compareAttribute);
            }
            else if (attribute is CreditCardAttribute creditCardAttribute)
            {
                adapter = new CreditCardClientValidator(creditCardAttribute);
            }
            else if (attribute is EmailAddressAttribute emailAddressAttribute)
            {
                adapter = new EmailAddressClientValidator(emailAddressAttribute);
            }
            else if (attribute is FileExtensionsAttribute fileExtensionsAttribute)
            {
                adapter = new FileExtensionsClientValidator(fileExtensionsAttribute);
            }
            else if (attribute is MaxLengthAttribute maxLengthAttribute)
            {
                adapter = new MaxLengthClientValidator(maxLengthAttribute);
            }
            else if (attribute is MinLengthAttribute minLengthAttribute)
            {
                adapter = new MinLengthClientValidator(minLengthAttribute);
            }
            else if (attribute is RangeAttribute rangeAttribute)
            {
                adapter = new RangeClientValidator(rangeAttribute, _options);
            }
            else if (attribute is RegularExpressionAttribute regularExpressionAttribute)
            {
                adapter = new RegularExpressionClientValidator(regularExpressionAttribute);
            }
            else if (attribute is RequiredAttribute requiredAttribute)
            {
                adapter = new RequiredClientValidator(requiredAttribute);
            }
            else if (attribute is StringLengthAttribute stringLengthAttribute)
            {
                adapter = new StringLengthClientValidator(stringLengthAttribute);
            }
            else if (attribute is UrlAttribute urlAttribute)
            {
                adapter = new UrlClientValidator(urlAttribute);
            }
            else
            {
                adapter = null;
            }
            
            return adapter;
        }
    }
}
