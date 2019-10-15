using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class CompareAttributeAdapter : VeeValidateAttributeAdapter<CompareAttribute>
    {
        public CompareAttributeAdapter(CompareAttribute attribute) : base(attribute)
        {
        }
        
        public override void AddValidation(ClientModelValidationContext context)
        {
            context.AddValidationRule("confirmed", $"'{Attribute.OtherProperty}'");
        }
    }
}
