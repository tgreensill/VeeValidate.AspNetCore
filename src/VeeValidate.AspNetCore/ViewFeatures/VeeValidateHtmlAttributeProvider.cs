using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;

namespace VeeValidate.AspNetCore.ViewFeatures
{
    public class VeeValidateHtmlAttributeProvider : DefaultValidationHtmlAttributeProvider
    {
        private readonly VeeValidateOptions _options;
        private readonly IDictionary<string, Func<string, ModelMetadata, string>> _adapters = new ConcurrentDictionary<string, Func<string, ModelMetadata, string>>();

        public VeeValidateHtmlAttributeProvider(
            IOptions<MvcViewOptions> optionsAccessor, 
            IModelMetadataProvider metadataProvider, 
            ClientValidatorCache clientValidatorCache, 
            VeeValidateOptions options,
            IEnumerable<IHtmlValidationAttributeAdapter> adapters) : base(optionsAccessor, metadataProvider, clientValidatorCache)
        {
            _options = options;

            // Convert the list of adapters into a dictionary of keys and functions.
            foreach (var adapter in adapters)
            {
                foreach (var key in adapter.Keys)
                {
                    // Only add one handler per key.
                    if (!_adapters.ContainsKey(key))
                    {
                        _adapters.Add(key, adapter.GetVeeValidateRule);
                    }
                }
            }
        }

        public override void AddValidationAttributes(ViewContext viewContext, ModelExplorer modelExplorer, IDictionary<string, string> attributes)
        {
            base.AddValidationAttributes(viewContext, modelExplorer, attributes);

            // Check whether attributes should be replaced.
            if (_options.ReplaceHtmlAttributes(viewContext))
            {
                if (attributes.TryGetValue("data-val", out string validate))
                {
                    if (validate == "true")
                    {
                        AddVeeValidateAttribute(modelExplorer, attributes);
                    }
                }

                // If there are any validation rules, adding by either the adapters or inline.
                if (attributes.ContainsKey("v-validate"))
                {
                    // Set data-vv-as attribute to give clean error description if not already set.
                    if (!attributes.ContainsKey("data-vv-as"))
                    {
                        attributes["data-vv-as"] = modelExplorer.Metadata.GetDisplayName();
                    }

                    // Add a class binding to toggle the input error class when the field is in an invalid state.
                    VueHtmlAttributeHelper.MergeClassAttribute(
                        attributes,
                        $"{{'{_options.ValidationInputCssClassName}': {_options.ErrorBagName}.has('{(attributes.ContainsKey("data-vv-name") ? attributes["data-vv-name"] : attributes["name"])}')}}");
                }
            }
        }

        private void AddVeeValidateAttribute(ModelExplorer modelExplorer, IDictionary<string, string> attributes)
        {
            ICollection<string> rules = new Collection<string>();

            // Get the model type adapter and add any attributes related to data type
            if (_adapters.TryGetValue("data-type", out var typeAdapter))
            {
                var rule = typeAdapter(null, modelExplorer.Metadata);
                if (!string.IsNullOrEmpty(rule))
                {
                    rules.Add(rule);
                }
            }

            // Convert the default validation attributes to vee validate attributes
            var validationAttributes = attributes.Where(x => x.Key.StartsWith("data-val")).ToArray();
            foreach (var attribute in validationAttributes)
            {
                if (_adapters.TryGetValue(attribute.Key, out var handler))
                {
                    var rule = handler(attribute.Value, modelExplorer.Metadata);
                    if (!string.IsNullOrEmpty(rule))
                    {
                        rules.Add(rule);
                    }
                }

                // Remove the default validation attribute
                attributes.Remove(attribute.Key);
            }

            // Merge the rules (including rules declared in the markup) into a single attribute.
            VueHtmlAttributeHelper.MergeVeeValidateAttributes(attributes, rules);
        }
    }
}
