using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Linq;

namespace VeeValidate.AspNetCore.Adapters
{
    public class FileExtensionsClientValidator : VeeAttributeAdapter<FileExtensionsAttribute>
    {
        public FileExtensionsClientValidator(FileExtensionsAttribute attribute) : base(attribute)
        {
        }

        public override void AddValidationRules(ClientModelValidationContext context)
        {
            if (!string.IsNullOrEmpty(Attribute.Extensions))
            {
                var extensions = Attribute.Extensions.Replace('|', ',')
                    .Split(',')
                    .Select(x => $"'{x}'");

                // Replace any pipe separators with commas
                MergeRule(context.Attributes, $"ext:[{string.Join(",", extensions)}]");
            }
            
        }
    }
}
