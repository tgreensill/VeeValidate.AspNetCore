using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Globalization;

namespace VeeValidate.AspNetCore.Adapters
{
    public class RangeAttributeAdapter : VeeAttributeAdapter<RangeAttribute>
    {
        private readonly string _max;
        private readonly string _min;
        private readonly VeeValidateOptions _options;

        public RangeAttributeAdapter(RangeAttribute attribute, VeeValidateOptions options) : base(attribute)
        {
            // Validate a randomly selected number.
            //attribute.IsValid(3);

            _options = options;
        }

        public override void AddValidationRules(ClientModelValidationContext context)
        {
            // This will trigger the conversion of Attribute.Minimum and Attribute.Maximum.
            // This is needed, because the attribute is stateful and will convert from a string like
            // "100m" to the decimal value 100.
            var max = Convert.ToString(Attribute.Maximum, CultureInfo.CurrentUICulture);
            var min = Convert.ToString(Attribute.Minimum, CultureInfo.CurrentUICulture);

            if (Attribute.OperandType == typeof(DateTime) || context.ModelMetadata.ModelType == typeof(DateTime))
            {
                if (DateTime.TryParse(min, out var minDate) && DateTime.TryParse(max, out var maxDate))
                {                    
                    // TODO - Date Format
                    MergeRule(context.Attributes, $"date_format:'{_options.DateTimeFormat}'"); // TODO - make this work with globalization
                    MergeRule(context.Attributes, $"date_between:{minDate.ToShortDateString()},{maxDate.ToShortDateString()},true");                    
                }
            }
            else
            {
                MergeRule(context.Attributes, $"max_value:{max}");
                MergeRule(context.Attributes, $"min_value:{min}");
            }
        }
    }
}
