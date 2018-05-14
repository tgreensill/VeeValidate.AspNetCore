using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using VeeValidate.AspNetCore.Adapters;

namespace VeeValidate.AspNetCore
{
    // TODO - Make sure modelstate error messages still generate
    // TODO - Phone validator - not supported out of the box, Numeric validator
    // TODO - Test localization
    public class VeeAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly VeeValidateOptions _options;

        public VeeAttributeAdapterProvider(VeeValidateOptions options)
        {
            _options = options;
        }

        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            IAttributeAdapter adapter;

            var type = attribute.GetType();

            if (type  == typeof(RegularExpressionAttribute))
            {
                adapter = new RegularExpressionClientValidator((RegularExpressionAttribute)attribute);
            }
            else if (type == typeof(MaxLengthAttribute))
            {
                adapter = new MaxLengthClientValidator((MaxLengthAttribute)attribute);
            }
            else if (type == typeof(RequiredAttribute))
            {
                adapter = new RequiredClientValidator((RequiredAttribute)attribute);
            }            
            else if (type == typeof(CompareAttribute))
            {
                adapter = new CompareClientValidator((CompareAttribute)attribute);
            }
            else if (type == typeof(MinLengthAttribute))
            {
                adapter = new MinLengthClientValidator((MinLengthAttribute)attribute);
            }
            else if (type == typeof(CreditCardAttribute))
            {
                adapter = new CreditCardClientValidator((CreditCardAttribute)attribute);                
            }
            else if (type == typeof(StringLengthAttribute))
            {
                adapter = new StringLengthClientValidator((StringLengthAttribute)attribute);
            }
            else if (type == typeof(RangeAttribute))
            {
                adapter = new RangeClientValidator((RangeAttribute)attribute, _options.Dates.Format);
            }
            else if (type == typeof(EmailAddressAttribute))
            {
                adapter = new EmailAddressClientValidator((EmailAddressAttribute)attribute);
            }
            //else if (type == typeof(PhoneAttribute))
            //{
            //    adapter = new DataTypeAttributeAdapter((DataTypeAttribute)attribute, "data-val-phone", stringLocalizer);
            //}
            else if (type == typeof(UrlAttribute))
            {
                adapter = new UrlClientValidator((UrlAttribute)attribute);
            }
            else if (type == typeof(FileExtensionsAttribute))
            {
                adapter = new FileExtensionsClientValidator((FileExtensionsAttribute)attribute);
            }
            else
            {
                adapter = null;
            }

            return adapter;
        }
    }
}
