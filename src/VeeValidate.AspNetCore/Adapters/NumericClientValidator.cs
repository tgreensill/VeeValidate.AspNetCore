using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class NumericClientValidator : VeeClientModelValidator
    {
        public override void AddValidationRules(ClientModelValidationContext context)
        {
            MergeValidationAttribute(context.Attributes, "numeric:true");            
        }
    }

    public class DecimalClientValidator : VeeClientModelValidator
    {
        public override void AddValidationRules(ClientModelValidationContext context)
        {
            MergeValidationAttribute(context.Attributes, "decimal:true");
        }
    }

    public class DateTimeClientValidator : VeeClientModelValidator
    {
        private readonly string _dateFormat;

        public DateTimeClientValidator(string dateFormat)
        {
            _dateFormat = dateFormat;
        }

        public override void AddValidationRules(ClientModelValidationContext context)
        {
            MergeValidationAttribute(context.Attributes, $"date_format:'{_dateFormat}'");
        }
    }    
}
