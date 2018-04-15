using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

namespace VeeValidate.AspNetCore.Adapters
{
    public class DataTypeAttributeAdapter : VeeAdapterBase<DataTypeAttribute>
    {
        private readonly string _rule;

        public DataTypeAttributeAdapter(DataTypeAttribute attribute, string rule, IStringLocalizer stringLocalizer, VeeValidateOptions options) : base(attribute, stringLocalizer, options)
        {
            _rule = rule;
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            MergeVeeValidateAttribute(context, _rule);            
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return GetErrorMessage(validationContext, Attribute.GetDataTypeName());
        }
    }
}
