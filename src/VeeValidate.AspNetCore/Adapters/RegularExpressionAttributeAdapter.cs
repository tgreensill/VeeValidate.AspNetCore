using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class RegularExpressionAttributeAdapter : VeeAttributeAdapter<RegularExpressionAttribute>
    {
        public RegularExpressionAttributeAdapter(RegularExpressionAttribute attribute) : base(attribute)
        {
        }

        public override void AddValidationRules(ClientModelValidationContext context)
        {
            // TODO - Escape backslashes when appropriate
            MergeRule(context.Attributes, $"regex:{Attribute.Pattern}");
        }
    }
}
