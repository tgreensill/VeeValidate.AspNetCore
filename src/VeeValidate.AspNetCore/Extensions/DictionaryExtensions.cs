using System.Collections.Generic;

namespace VeeValidate.AspNetCore
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Adds the item to the dictionary if the key is not found.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Merge(this IDictionary<string, string> dictionary, string key, string value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
            }                
        }
    }
}
