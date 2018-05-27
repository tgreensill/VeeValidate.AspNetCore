using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore.Adapters
{
    public class FileExtensionsAttributeAdapter : IHtmlValidationAttributeAdapter, IHtmlInputTypeAttributeAdapter
    {
        public string[] Attributes => new [] { "data-val-fileextensions-extensions" };

        public string[] InputTypes => new[] { "file" };

        public void AddVeeValidateRules(string value, ModelMetadata metadata, IDictionary<string, string> rules)
        {
            if (value == "file")
            {
                var extensionsAttribute = metadata.ValidatorMetadata?.FirstOrDefault(x => x is FileExtensionsAttribute);
                if (extensionsAttribute != null)
                {
                    value = ((FileExtensionsAttribute)extensionsAttribute).Extensions;
                }
            }

            var extensions = value.Split(',').Select(x => $"'{x}'");
            rules.Merge("ext", $"[{string.Join(",", extensions)}]");            
        }
    }
}
