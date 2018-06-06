using System;
using System.Collections.Generic;

namespace VeeValidate.AspNetCore.ViewFeatures
{
    public class VueHtmlAttributeHelper
    {
        private const string VeeValidateAttributeName = "v-validate";

        public static void MergeClassAttribute(IDictionary<string, string> attributes, string attributeValue)
        {
            MergeClassOrStylesAttribute(attributes, "class", attributeValue);
        }

        public static void MergeStylesAttribute(IDictionary<string, string> attributes, string attributeValue)
        {
            MergeClassOrStylesAttribute(attributes, "styles", attributeValue);
        }

        public static void MergeVeeValidateAttribute(IDictionary<string, string> attributes, string ruleName, string ruleValue)
        {
            // Merge on v-validate field.
            if (attributes.TryGetValue(VeeValidateAttributeName, out var rules))
            {
                if (rules.TrimStart().StartsWith("'"))
                {
                    throw new Exception("VeeValidate rules cannot be merged because v-validate rules are not in object format, i.e. '{}'.");
                }

                attributes[VeeValidateAttributeName] = $"{{{rules.TrimStart('{').TrimEnd('}')},{ruleName}:{ruleValue}}}";
                return;
            }

            attributes.Add(VeeValidateAttributeName, $"{{{ruleName}:{ruleValue}}}");
        }

        #region { Private }

        private static void MergeClassOrStylesAttribute(IDictionary<string, string> attributes, string attributeName, string attributeValue)
        {
            // Binding keys can come in two flavours: v-bind:{key} and :{key}.
            if (attributes.ContainsKey($":{attributeName}"))
            {
                MergeClassOrStylesAttributeImpl(attributes, $":{attributeName}", attributeValue);
                
                return;
            }

            // Default to v-bind: notation unless already specified in the markup
            MergeClassOrStylesAttributeImpl(attributes, $"v-bind:{attributeName}", attributeValue);
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
