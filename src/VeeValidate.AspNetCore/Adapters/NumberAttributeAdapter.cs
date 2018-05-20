using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class NumberAttributeAdapter : IHtmlValidationAttributeAdapter
    {
        public string[] Keys => new [] { "data-val-number" };

        public string GetVeeValidateRule(string value, ModelMetadata metadata)
        {
            var typeToValidate = metadata.UnderlyingOrModelType;

            // Check only the numeric types for which we set type='text'.
            if (typeToValidate == typeof(float) ||
                typeToValidate == typeof(double) ||
                typeToValidate == typeof(decimal))
            {
                return "decimal:true";
            }
                
            return "numeric:true";
        }
    }   
}
