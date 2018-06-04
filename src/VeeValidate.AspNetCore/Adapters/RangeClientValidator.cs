using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class RangeClientValidator : VeeValidateAttributeAdapter<RangeAttribute>
    {
        private readonly VeeValidateOptions _options;
        
        public RangeClientValidator(RangeAttribute attribute, VeeValidateOptions options) : base(attribute)
        {
            _options = options;
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-vv-as", context.ModelMetadata.GetDisplayName());

            // This will trigger the conversion of Attribute.Minimum and Attribute.Maximum.
            // This is needed, because the attribute is stateful and will convert from a string like
            // "100m" to the decimal value 100.
            var max = Convert.ToString(Attribute.Maximum, CultureInfo.CurrentUICulture);
            var min = Convert.ToString(Attribute.Minimum, CultureInfo.CurrentUICulture);
            
            if (context.ModelMetadata.UnderlyingOrModelType == typeof(DateTime) || (Nullable.GetUnderlyingType(Attribute.OperandType) ?? Attribute.OperandType) == typeof(DateTime))
            {
                var normalisedDateFormat = _options.Dates.Format.Replace('D', 'd').Replace('Y', 'y');

                MergeValidationAttribute(context.Attributes, "date_format", $"'{_options.Dates.Format}'");

                if (DateTime.TryParse(min, out var minDate))
                {
                    MergeValidationAttribute(context.Attributes, "after", $"['{minDate.ToString(normalisedDateFormat)}',true]");
                }

                if (DateTime.TryParse(max, out var maxDate))
                {
                    MergeValidationAttribute(context.Attributes, "before", $"['{maxDate.ToString(normalisedDateFormat)}',true]");
                }
            }
            else
            {
                MergeValidationAttribute(context.Attributes, "max_value", max);
                MergeValidationAttribute(context.Attributes, "min_value", min);
            }
        }
    }
}
