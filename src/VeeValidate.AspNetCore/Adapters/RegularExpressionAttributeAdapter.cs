using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class RegularExpressionAttributeAdapter : IHtmlValidationAttributeAdapter
    {
        public string[] Keys => new [] { "data-val-regex-pattern" };

        public string GetVeeValidateRule(string value, ModelMetadata metadata)
        {
            return $"regex:/{value.Trim('/')}/";
        }
    }
}
