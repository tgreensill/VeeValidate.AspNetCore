using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class RangeAttributeAdapter : VeeValidateAttributeAdapter<RangeAttribute>
    {
        private readonly VeeValidateOptions _options;
        
        public RangeAttributeAdapter(RangeAttribute attribute, VeeValidateOptions options) : base(attribute)
        {
            _options = options;
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            context.AddValidationDisplayName();

            // This will trigger the conversion of Attribute.Minimum and Attribute.Maximum.
            // This is needed, because the attribute is stateful and will convert from a string like
            // "100m" to the decimal value 100.
            var max = Convert.ToString(Attribute.Maximum, CultureInfo.CurrentUICulture);
            var min = Convert.ToString(Attribute.Minimum, CultureInfo.CurrentUICulture);
            
            if (context.ModelMetadata.UnderlyingOrModelType == typeof(DateTime) || (Nullable.GetUnderlyingType(Attribute.OperandType) ?? Attribute.OperandType) == typeof(DateTime))
            {
                var dateFormat = _options.DateFormatProvider(context.ActionContext.HttpContext);
                var normalisedDateFormat = dateFormat.Replace('D', 'd').Replace('Y', 'y');

                context.AddValidationRule("date_format", $"'{dateFormat}'");

                if (DateTime.TryParse(min, out var minDate))
                {
                    context.AddValidationRule("after", $"['{minDate.ToString(normalisedDateFormat)}',true]");
                }

                if (DateTime.TryParse(max, out var maxDate))
                {
                    context.AddValidationRule("before", $"['{maxDate.ToString(normalisedDateFormat)}',true]");
                }
            }
            else
            {
                context
                    .AddValidationRule("max_value", max)
                    .AddValidationRule("min_value", min);
            }
        }
    }
}
