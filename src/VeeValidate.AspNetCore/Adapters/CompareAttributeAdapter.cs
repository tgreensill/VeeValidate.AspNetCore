using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class CompareAttributeAdapter : IHtmlValidationAttributeAdapter
    {
        public string[] Keys => new [] { "data-val-equalto-other" };

        public string GetVeeValidateRule(string value, ModelMetadata metadata)
        {
            return $"confirmed:{value}";
        }
    }
}
