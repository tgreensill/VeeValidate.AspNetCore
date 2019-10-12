using VeeValidate.AspNetCore.ViewFeatures;

namespace Microsoft.AspNetCore.Mvc.ModelBinding.Validation
{
    public static class ClientModelValidationContextExtensions
    {
        private const string VeeValidationDisplayAsAttribute = "data-vv-as";

        public static ClientModelValidationContext AddValidationDisplayName(this ClientModelValidationContext context)
        {
            if (!context.Attributes.ContainsKey(VeeValidationDisplayAsAttribute))
            {
                context.Attributes.Add(VeeValidationDisplayAsAttribute, context.ModelMetadata.GetDisplayName());
            }

            return context;
        }

        public static ClientModelValidationContext AddValidationRule(this ClientModelValidationContext context, string ruleName, object value)
        {
            VueHtmlAttributeHelper.MergeVeeValidateAttribute(context.Attributes, ruleName, value.ToString());
            return context;
        }
    }
}
