using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using VeeValidate.AspNetCore.Adapters;

namespace VeeValidate.AspNetCore
{
    public class VeeValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly VeeValidateOptions _options;

        public VeeValidationAttributeAdapterProvider(VeeValidateOptions options)
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

            if (type == typeof(RegularExpressionAttribute))
            {
                adapter = new RegularExpressionAttributeAdapter((RegularExpressionAttribute)attribute);
            }
            else if (type == typeof(MaxLengthAttribute))
            {
                adapter = new MaxLengthAttributeAdapter((MaxLengthAttribute)attribute);
            }
            else if (type == typeof(RequiredAttribute))
            {
                adapter = new RequiredAttributeAdapter((RequiredAttribute)attribute);
            }            
            else if (type == typeof(CompareAttribute))
            {
                adapter = new CompareAttributeAdapter((CompareAttribute)attribute);
            }
            else if (type == typeof(MinLengthAttribute))
            {
                adapter = new MinLengthAttributeAdapter((MinLengthAttribute)attribute);
            }
            else if (type == typeof(CreditCardAttribute))
            {
                adapter = new DataTypeAttributeAdapter((DataTypeAttribute)attribute, "credit_card:true");
            }
            else if (type == typeof(StringLengthAttribute))
            {
                adapter = new StringLengthAttributeAdapter((StringLengthAttribute)attribute);
            }
            else if (type == typeof(RangeAttribute))
            {
                adapter = new RangeAttributeAdapter((RangeAttribute)attribute, _options);
            }
            else if (type == typeof(EmailAddressAttribute))
            {
                adapter = new DataTypeAttributeAdapter((DataTypeAttribute)attribute, "email:true");
            }
            //else if (type == typeof(PhoneAttribute))
            //{
            //    adapter = new DataTypeAttributeAdapter((DataTypeAttribute)attribute, "data-val-phone", stringLocalizer);
            //}
            else if (type == typeof(UrlAttribute))
            {
                adapter = new DataTypeAttributeAdapter((DataTypeAttribute)attribute, $"url:true");
            }
            //else if (type == typeof(FileExtensionsAttribute))
            //{
            //    adapter = new FileExtensionsAttributeAdapter((FileExtensionsAttribute)attribute, stringLocalizer);
            //}            
            else
            {
                adapter = null;
            }

            return adapter;
        }
    }
}
