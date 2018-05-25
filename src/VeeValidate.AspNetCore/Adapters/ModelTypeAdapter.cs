using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    /// <summary>
    /// DotNetCore doesn't add the validation attributes for the data type, i.e. data-val-date, data-val-number, etc.
    /// This is a blanket adapter to add any rules related to the field data type.
    /// </summary>
    public class ModelTypeAdapter : IHtmlValidationAttributeAdapter
    {
        private readonly VeeValidateOptions _options;
        
        public ModelTypeAdapter(VeeValidateOptions options)
        {
            _options = options;
        }

        // Use a special key that is not associated with any jQuery validation rules.
        public string[] Keys => new[] { "data-type" };

        public string GetVeeValidateRule(string value, ModelMetadata metadata)
        {
            var typeToValidate = metadata.UnderlyingOrModelType;

            if (typeToValidate == typeof(DateTime))
            {
                return $"date_format:'{_options.Dates.Format}'";
            }
            
            if (typeToValidate == typeof(float) ||
                typeToValidate == typeof(double) ||
                typeToValidate == typeof(decimal))
            {
                return "decimal:true";
            }

            if (typeToValidate == typeof(short) ||
                typeToValidate == typeof(int) ||
                typeToValidate == typeof(long))
            {
                return "numeric:true";
            }

            return null;
        }
    }
}
