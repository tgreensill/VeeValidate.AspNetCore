﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class RangeMaxAttributeAdapter : IHtmlValidationAttributeAdapter
    {
        private readonly VeeValidateOptions _options;
        public RangeMaxAttributeAdapter(VeeValidateOptions options)
        {
            _options = options;
        }

        public string[] Attributes => new[] { "data-val-range-max" };

        public void AddVeeValidateRules(string value, ModelMetadata metadata, IDictionary<string, string> rules)
        {
            // Fluent Validation leaves the value as declared??
            if (metadata.UnderlyingOrModelType == typeof(DateTime))
            {
                // AspNetCore uses InvariantCulture for formatting dates
                // https://github.com/aspnet/Mvc/blob/dev/src/Microsoft.AspNetCore.Mvc.DataAnnotations/Internal/RangeAttributeAdapter.cs
                if (DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault, out var date))
                {
                    var normalisedDateFormat = _options.Dates.Format.Replace('D', 'd').Replace('Y', 'y');

                    rules.Merge("date_format", $"'{_options.Dates.Format}'");
                    rules.Merge("before", $"['{date.ToString(normalisedDateFormat)}',true]");
                    return;
                }
            }

            rules.Merge("max_value", $"'{value}'");            
        }
    }
}
