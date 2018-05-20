using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class EmailAddressAttributeAdapter : IHtmlValidationAttributeAdapter
    {
        public string[] Keys => new [] { "data-val-email" };
        public string GetVeeValidateRule(string value, ModelMetadata metadata)
        {
            return "email:true";
        }
    }
}
