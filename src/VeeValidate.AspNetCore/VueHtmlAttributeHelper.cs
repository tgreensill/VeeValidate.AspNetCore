using System.Collections.Generic;

namespace VeeValidate.AspNetCore
{
    public class VueHtmlAttributeHelper
    {
        public static void MergeBindAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            // Binding keys can come in two flavours: v-bind:{key} and :{key}.
            if (attributes.TryGetValue($"v-bind:{key}", out var existingValue))
            {
                // Preserve the existing attribute name
                attributes[$"v-bind:{key}"] = MergeVeeBindAttributeValue(existingValue, value);
                return;
            }

            if (attributes.TryGetValue($":{key}", out existingValue))
            {
                attributes[$":{key}"] = MergeVeeBindAttributeValue(existingValue, value);
                return;
            }

            // Use the short-hand version of the attribute by default
            attributes.Add($":{key}", value);
        }

        private static string MergeVeeBindAttributeValue(string existingValue, string newValue)
        {
            // Binding values an come in three flavours: string, object '{}', and array '[]'.
            // Merge the values together into the array format.
            return $"[{string.Join(",", existingValue.TrimStart('[').TrimEnd(']'), newValue.TrimStart('[').TrimEnd(']'))}]";
        }
    }
}
