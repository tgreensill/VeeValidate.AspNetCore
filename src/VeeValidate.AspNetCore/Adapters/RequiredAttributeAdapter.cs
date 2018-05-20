using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class RequiredAttributeAdapter : IHtmlValidationAttributeAdapter
    {
        public string[] Keys => new [] { "data-val-required" };

        public string GetVeeValidateRule(string value, ModelMetadata metadata)
        {
            return "required:true";
        }
    }
}
