using System;
using System.Collections.Generic;
using System.Linq;

namespace VeeValidate.AspNetCore
{
    public class VueHtmlAttributeHelper
    {
        public static void MergeClassAttribute(IDictionary<string, string> attributes, string attributeValue)
        {
            MergeClassOrStylesAttribute(attributes, "class", attributeValue);
        }

        public static void MergeStylesAttribute(IDictionary<string, string> attributes, string attributeValue)
        {
            MergeClassOrStylesAttribute(attributes, "styles", attributeValue);
        }

        public static void MergeVeeValidateAttributes(IDictionary<string, string> attributes, IDictionary<string, string> validationRules)
        {
            var rules = string.Join(",", validationRules.Select(x => $"{x.Key}:{x.Value}"));

            // Get any existing rules declared in the markup.
            if (attributes.TryGetValue("v-validate", out var existingRules))
            {
                // Prevent users declaring inline validation rules using the shorthand string format.
                if (!existingRules.StartsWith("{"))
                {
                    throw new Exception("v-validate attributes must be declared in object format.");
                }

                // TODO - Filter out any validation rules that are already in the existing rules...
                attributes["v-validate"] = "{" + existingRules.TrimStart('{').TrimEnd('}') + "," + rules + "}";
                return;
            }

            // Use the object format for the rules as this is the safe format for regex rules.
            attributes["v-validate"] = "{" + rules + "}";
        }

        #region { Private }

        private static void MergeClassOrStylesAttribute(IDictionary<string, string> attributes, string attributeName, string attributeValue)
        {
            // Binding keys can come in two flavours: v-bind:{key} and :{key}.
            if (attributes.ContainsKey($"v-bind:{attributeName}"))
            {
                MergeClassOrStylesAttributeImpl(attributes, $"v-bind:{attributeName}", attributeValue);
                return;
            }

            // Default to short-hand notation unless already specified in the markup
            MergeClassOrStylesAttributeImpl(attributes, $":{attributeName}", attributeValue);
        }

        private static void MergeClassOrStylesAttributeImpl(IDictionary<string, string> attributes, string attributeName, string attributeValue)
        {
            // If the attribute does not exist in the list.
            if (!attributes.TryGetValue(attributeName, out string existingValue))
            {
                // Add the value as is.
                attributes[attributeName] = attributeValue;
                return;
            }

            // Binding values can come in three flavours: string, object '{}', and array '[]'.
            // Merge the values together into the array format.
            attributes[attributeName] = $"[{string.Join(",", existingValue.TrimStart('[').TrimEnd(']'), attributeValue.TrimStart('[').TrimEnd(']'))}]";
        }
        
        #endregion
    }
}
