﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace VeeValidate.AspNetCore
{
    public interface IHtmlValidationAttributeAdapter
    {
        /// <summary>
        /// A list of html "data-val*" attributes that the adapter is for.
        /// </summary>
        string[] Attributes { get; }
        
        /// <summary>
        /// Gets the matching vee-validate rule for the jquery validation attribute.
        /// </summary>
        /// <param name="value">The jquery validation attribute value.</param>
        /// <param name="metadata"></param>
        /// <returns>The vee-validate rule.</returns>
        void AddVeeValidateRules(string value, ModelMetadata metadata, IDictionary<string, string> rules);
    }
}
