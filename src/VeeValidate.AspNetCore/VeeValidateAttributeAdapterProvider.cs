using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
                adapter = new CompareAttributeAdapter(compareAttribute);
            }
            else if (attribute is CreditCardAttribute creditCardAttribute)
            {
                adapter = new CreditCardAttributeAdapter(creditCardAttribute);
            }
            else if (attribute is EmailAddressAttribute emailAddressAttribute)
            {
                adapter = new EmailAddressAttributeAdapter(emailAddressAttribute);
            }
            else if (attribute is FileExtensionsAttribute fileExtensionsAttribute)
            {
                adapter = new FileExtensionsAttributeAdapter(fileExtensionsAttribute);
            }
            else if (attribute is MaxLengthAttribute maxLengthAttribute)
            {
                adapter = new MaxLengthAttributeAdapter(maxLengthAttribute);
            }
            else if (attribute is MinLengthAttribute minLengthAttribute)
            {
                adapter = new MinLengthAttributeAdapter(minLengthAttribute);
            }
            else if (attribute is RangeAttribute rangeAttribute)
            {
                adapter = new RangeAttributeAdapter(rangeAttribute, _options);
            }
            else if (attribute is RegularExpressionAttribute regularExpressionAttribute)
            {
                adapter = new RegularExpressionAttributeAdapter(regularExpressionAttribute);
            }
            else if (attribute is RequiredAttribute requiredAttribute)
            {
                adapter = new RequiredAttributeAdapter(requiredAttribute);
            }
            else if (attribute is StringLengthAttribute stringLengthAttribute)
            {
                adapter = new StringLengthAttributeAdapter(stringLengthAttribute);
            }
            else if (attribute is UrlAttribute urlAttribute)
            {
                adapter = new UrlAttributeAdapter(urlAttribute);
            }
            else
            {
                adapter = null;
            }
            
            return adapter;
        }
    }
}
