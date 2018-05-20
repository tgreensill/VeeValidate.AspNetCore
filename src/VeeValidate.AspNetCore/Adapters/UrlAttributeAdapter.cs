using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class UrlAttributeAdapter : IHtmlValidationAttributeAdapter
    {
        public string[] Keys => new [] { "data-val-url" };

        public string GetVeeValidateRule(string value, ModelMetadata metadata)
        {
            return "url:[true,true]";
        }
    }
}
