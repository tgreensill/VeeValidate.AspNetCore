using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class DataTypeAttributeAdapter : VeeAttributeAdapter<DataTypeAttribute>
    {
        private readonly string _rule;

        public DataTypeAttributeAdapter(DataTypeAttribute attribute, string rule) : base(attribute)
        {
            _rule = rule;
        }

        public override void AddValidationRules(ClientModelValidationContext context)
        {
            MergeRule(context.Attributes, _rule);
        }

    }
}
