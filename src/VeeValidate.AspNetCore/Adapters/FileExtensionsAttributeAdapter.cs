using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class FileExtensionsAttributeAdapter : IHtmlValidationAttributeAdapter
    {
        public string[] Keys => new [] { "data-val-fileextensions-extensions" };
        public string GetVeeValidateRule(string value, ModelMetadata metadata)
        {
            var extensions = value.Split(',').Select(x => $"'{x}'");
            return $"ext:[{string.Join(",", extensions)}]";
        }
    }
}
