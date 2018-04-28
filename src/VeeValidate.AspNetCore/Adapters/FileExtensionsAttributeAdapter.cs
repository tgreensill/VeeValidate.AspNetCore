using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class FileExtensionsAttributeAdapter : VeeAttributeAdapter<FileExtensionsAttribute>
    {
        public FileExtensionsAttributeAdapter(FileExtensionsAttribute attribute) : base(attribute)
        {
        }

        public override void AddValidationRules(ClientModelValidationContext context)
        {
            if (!string.IsNullOrEmpty(Attribute.Extensions))
            {
                // Replace any pipe separators with commas
                MergeRule(context.Attributes, $"ext:[{Attribute.Extensions.Replace('|',',')}]");
            }
            
        }
    }
}
