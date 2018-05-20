using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class RangeMaxAttributeAdapter : IHtmlValidationAttributeAdapter
    {
        private readonly string _dateFormat;
        public RangeMaxAttributeAdapter(VeeValidateOptions options)
        {
            _dateFormat = options.Dates.Format;
        }

        public string[] Keys => new[] { "data-val-range-max" };
        public string GetVeeValidateRule(string value, ModelMetadata metadata)
        {
            if (metadata.UnderlyingOrModelType == typeof(DateTime))
            {
                if (DateTime.TryParse(value, out var date))
                {
                    var normalisedDateFormat = _dateFormat.Replace('D', 'd').Replace('Y', 'y');

                    return $"before:['{date.ToString(normalisedDateFormat)}',true]";
                }
            }
            
            return $"max_value:'{value}'";
        }
    }
}
