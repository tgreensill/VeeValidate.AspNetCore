using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class RangeMinAttributeAdapter : IHtmlValidationAttributeAdapter
    {
        private readonly VeeValidateOptions _options;
        public RangeMinAttributeAdapter(VeeValidateOptions options)
        {
            _options = options;
        }

        public string[] Keys => new[] { "data-val-range-min" };
        public string GetVeeValidateRule(string value, ModelMetadata metadata)
        {
            // Fluent Validation leaves the value as declared??
            if (metadata.UnderlyingOrModelType == typeof(DateTime))
            {
                // AspNetCore uses InvariantCulture for formatting dates
                // https://github.com/aspnet/Mvc/blob/dev/src/Microsoft.AspNetCore.Mvc.DataAnnotations/Internal/RangeAttributeAdapter.cs
                if (DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault, out var date))
                {
                    var normalisedDateFormat = _options.Dates.Format.Replace('D', 'd').Replace('Y', 'y');

                    return $"after:['{date.ToString(normalisedDateFormat)}',true]";
                }
            }

            return $"min_value:'{value}'";
        }
    }
}
