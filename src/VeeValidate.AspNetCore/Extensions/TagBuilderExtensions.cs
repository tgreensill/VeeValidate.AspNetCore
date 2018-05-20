//using Microsoft.AspNetCore.Mvc.Rendering;

//// ReSharper disable once CheckNamespace
//namespace VeeValidate.AspNetCore
//{
//    public static class TagBuilderExtensions
//    {
//        public static void MergeVeeBindAttribute(this TagBuilder tagBuilder, string key, string value)
//        {
//            // Binding keys can come in two flavours: v-bind:{key} and :{key}.
//            if (tagBuilder.Attributes.TryGetValue($"v-bind:{key}", out var existingValue))
//            {
//                // Preserve the existing attribute name
//                tagBuilder.Attributes[$"v-bind:{key}"] = MergeVeeBindAttributeValue(existingValue, value);
//                return;
//            }
            
//            if (tagBuilder.Attributes.TryGetValue($":{key}", out existingValue))
//            {
//                tagBuilder.Attributes[$":{key}"] = MergeVeeBindAttributeValue(existingValue, value);
//                return;
//            }

//            // Use the short-hand version ofthe attribute by default
//            tagBuilder.Attributes.Add($":{key}", value);
//        }

//        private static string MergeVeeBindAttributeValue(string existingValue, string newValue)
//        {
//            // Binding values an come in three flavours: string, object '{}', and array '[]'.
//            // Merge the values together into the array format.
//            return $"[{string.Join(",", existingValue.TrimStart('[').TrimEnd(']'), newValue.TrimStart('[').TrimEnd(']'))}]"; 
//        }
//    }
//}
