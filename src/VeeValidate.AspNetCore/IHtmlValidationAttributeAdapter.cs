using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VeeValidate.AspNetCore
{
    public interface IHtmlValidationAttributeAdapter
    {
        /// <summary>
        /// The attribute names that the adapter is for.
        /// </summary>
        string[] Keys { get; }
        
        /// <summary>
        /// Gets the matching vee-validate rule for the jquery validation attribute.
        /// </summary>
        /// <param name="value">The jquery validation attribute value.</param>
        /// <param name="metadata"></param>
        /// <returns>The vee-validate rule.</returns>
        string GetVeeValidateRule(string value, ModelMetadata metadata);
    }
}
