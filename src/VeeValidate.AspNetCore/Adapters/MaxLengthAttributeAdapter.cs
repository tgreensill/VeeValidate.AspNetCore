using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class MaxLengthAttributeAdapter : IHtmlValidationAttributeAdapter
    {
        public string[] Keys => new []
        {
            "data-val-maxlength-max", "data-val-length-max"
        };
        public string GetVeeValidateRule(string value, ModelMetadata metadata)
        {
            return $"max:{value}";
        }
    }
}
