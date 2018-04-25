using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;

namespace VeeValidate.AspNetCore.Adapters
{
    public class MinLengthAttributeAdapter : VeeAttributeAdapter<MinLengthAttribute>
    {
        public MinLengthAttributeAdapter(MinLengthAttribute attribute) : base(attribute)
        {
        }

        public override void AddValidationRules(ClientModelValidationContext context)
        {
            MergeRule(context.Attributes, $"min:{Attribute.Length}");
        }
    }
}
