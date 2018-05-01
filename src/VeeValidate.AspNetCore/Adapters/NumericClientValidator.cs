using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class NumericClientValidator : VeeClientModelValidator
    {
        public override void AddValidationRules(ClientModelValidationContext context)
        {
            MergeRule(context.Attributes, "numeric:true");            
        }
    }

    public class DecimalClientValidator : VeeClientModelValidator
    {
        public override void AddValidationRules(ClientModelValidationContext context)
        {
            MergeRule(context.Attributes, "decimal:true");
        }
    }
}
