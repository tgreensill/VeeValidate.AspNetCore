using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class RangeMinAttributeAdapter : IHtmlValidationAttributeAdapter
    {
        private readonly string _dateFormat;
        public RangeMinAttributeAdapter(VeeValidateOptions options)
        {
            _dateFormat = options.Dates.Format;
        }

        public string[] Keys => new[] { "data-val-range-min" };
        public string GetVeeValidateRule(string value, ModelMetadata metadata)
        {
            if (metadata.UnderlyingOrModelType == typeof(DateTime))
            {
                if (DateTime.TryParse(value, out var date))
                {
                    var normalisedDateFormat = _dateFormat.Replace('D', 'd').Replace('Y', 'y');

                    return $"after:['{date.ToString(normalisedDateFormat)}',true]";
                }
            }

            return $"min_value:'{value}'";
        }
    }
}
