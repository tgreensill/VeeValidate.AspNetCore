using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class DataTypeClientValidator : VeeAttributeClientValidator<DataTypeAttribute>
    {
        private readonly string _rule;

        public DataTypeClientValidator(DataTypeAttribute attribute, string rule) : base(attribute)
        {
            _rule = rule;
        }

        public override void AddValidationRules(ClientModelValidationContext context)
        {
            MergeRule(context.Attributes, _rule);
        }
    }
}
