using VeeValidate.AspNetCore.ViewFeatures;

namespace Microsoft.AspNetCore.Mvc.ModelBinding.Validation
{
    public static class ClientModelValidationContextExtensions
    {
        public static ClientModelValidationContext AddValidationRule(this ClientModelValidationContext context, string ruleName, object value)
        {
            return AddValidationRule(context, ruleName, value.ToString());
        }

        public static ClientModelValidationContext AddValidationRule(this ClientModelValidationContext context, string ruleName, string value)
        {
            VueHtmlAttributeHelper.MergeVeeValidateAttribute(context.Attributes, ruleName, value.ToString());
            return context;
        }
    }
}
