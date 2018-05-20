using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class MinLengthAttributeAdapter : IHtmlValidationAttributeAdapter
    {
        public string[] Keys => new []
        {
            "data-val-minlength-min", "data-val-length-min" 
        };

        public string GetVeeValidateRule(string value, ModelMetadata metadata)
        {
            return $"min:{value}";
        }
    }
}
