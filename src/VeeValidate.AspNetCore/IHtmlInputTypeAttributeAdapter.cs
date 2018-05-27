using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace VeeValidate.AspNetCore
{
    public interface IHtmlInputTypeAttributeAdapter
    {
        /// <summary>
        /// A list of html input types that the adapter is for.
        /// </summary>
        string[] InputTypes { get; }

        /// <summary>
        /// Gets the appropriate vee-validate rules for the input type.
        /// </summary>
        /// <param name="value">The type attribute value.</param>
        /// <param name="metadata"></param>
        /// <returns>The vee-validate rule.</returns>
        void AddVeeValidateRules(string value, ModelMetadata metadata, IDictionary<string, string> rules);
    }
}
