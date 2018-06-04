using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class FileExtensionsAttributeAdapter : VeeValidateAttributeAdapter<FileExtensionsAttribute>
    {
        public FileExtensionsAttributeAdapter(FileExtensionsAttribute attribute) : base(attribute)
        {
        }
        
        public override void AddValidation(ClientModelValidationContext context)
        {
            if (!string.IsNullOrEmpty(Attribute.Extensions))
            {
                // Convert to Vee-Validate format, i.e. "jpg,gif" to 'jpg','gif'
                var extensions = Attribute.Extensions.Replace('|', ',') 
                    .Split(',')
                    .Select(x => $"'{x}'"); 
                
                MergeAttribute(context.Attributes, "data-vv-as", context.ModelMetadata.GetDisplayName());
                MergeValidationAttribute(context.Attributes, "ext", $"[{string.Join(",", extensions)}]");
            }
        }
    }
}
