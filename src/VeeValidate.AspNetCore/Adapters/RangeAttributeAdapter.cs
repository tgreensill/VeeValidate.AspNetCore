using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace VeeValidate.AspNetCore.Adapters
{
    public class RangeAttributeAdapter : VeeAdapterBase<RangeAttribute>
    {
        private readonly string _max;
        private readonly string _min;
        private readonly VeeValidateOptions _options;

        public RangeAttributeAdapter(RangeAttribute attribute, IStringLocalizer stringLocalizer, VeeValidateOptions options) : base(attribute, stringLocalizer, options)
        {
            // This will trigger the conversion of Attribute.Minimum and Attribute.Maximum.
            // This is needed, because the attribute is stateful and will convert from a string like
            // "100m" to the decimal value 100.
            
            // Validate a randomly selected number.
            attribute.IsValid(3);

            _max = Convert.ToString(Attribute.Maximum, CultureInfo.CurrentUICulture);
            _min = Convert.ToString(Attribute.Minimum, CultureInfo.CurrentUICulture);
            _options = options;
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            
            if (Attribute.OperandType == typeof(DateTime) || context.ModelMetadata.ModelType == typeof(DateTime))
            {
                MergeVeeValidateAttribute(context, $"date_format:{_options.DateTimeFormat}");
                //MergeVeeValidateAttribute(context, $"before:{Attribute.Maximum}"); // max - 1
                //MergeVeeValidateAttribute(context, $"after:{Attribute.Minimum}"); // min + 1
            }
            else
            {
                MergeVeeValidateAttribute(context, $"max_value:{Attribute.Maximum}");
                MergeVeeValidateAttribute(context, $"min_value:{Attribute.Minimum}");
            }
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return GetErrorMessage(validationContext, Attribute.Minimum, Attribute.Maximum);
        }
    }
}
