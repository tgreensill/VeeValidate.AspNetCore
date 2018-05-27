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
        private readonly IDictionary<string, Action<string, ModelMetadata, IDictionary<string, string>>> _adapters = new ConcurrentDictionary<string, Action<string, ModelMetadata, IDictionary<string,string>>>();
        private readonly IDictionary<string, Action<string, ModelMetadata, IDictionary<string, string>>> _inputTypeAdapters = new ConcurrentDictionary<string, Action<string, ModelMetadata, IDictionary<string, string>>>();

        public VeeValidateHtmlAttributeProvider(
            IOptions<MvcViewOptions> optionsAccessor, 
            IModelMetadataProvider metadataProvider, 
            ClientValidatorCache clientValidatorCache, 
            VeeValidateOptions options,
            IEnumerable<IHtmlValidationAttributeAdapter> adapters,
            IEnumerable<IHtmlInputTypeAttributeAdapter> inputTypeAdapters) : base(optionsAccessor, metadataProvider, clientValidatorCache)
        {
            _options = options;

            // Convert the list of adapters into a dictionary of keys and functions.
            foreach (var adapter in adapters)
            {
                foreach (var key in adapter.Attributes)
                {
                    // Only add one handler per key.
                    if (!_adapters.ContainsKey(key))
                    {
                        _adapters.Add(key, adapter.AddVeeValidateRules);
                    }
                }
            }

            foreach (var adapter in inputTypeAdapters)
            {
                foreach (var key in adapter.InputTypes)
                {
                    // Only add one handler per key.
                    if (!_inputTypeAdapters.ContainsKey(key))
                    {
                        _inputTypeAdapters.Add(key, adapter.AddVeeValidateRules);
                    }
                }
            }
        }

        public override void AddValidationAttributes(ViewContext viewContext, ModelExplorer modelExplorer, IDictionary<string, string> attributes)
        {
            base.AddValidationAttributes(viewContext, modelExplorer, attributes);
            
            AddVeeValidateAttribute(modelExplorer, attributes);                

            // If there are any validation rules, added by either the adapters or inline via the html.
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

        private void AddVeeValidateAttribute(ModelExplorer modelExplorer, IDictionary<string, string> attributes)
        {
            IDictionary<string, string> rules = new Dictionary<string, string>();

            if (attributes.TryGetValue("data-val", out string validate))
            {
                if (validate == "true")
                {
                    // Convert the default validation attributes to vee validate attributes
                    var validationAttributes = attributes.Where(x => x.Key.StartsWith("data-val")).ToArray();
                    foreach (var attribute in validationAttributes)
                    {
                        if (_adapters.TryGetValue(attribute.Key, out var handler))
                        {
                            handler(attribute.Value, modelExplorer.Metadata, rules);
                        }

                        // Remove the default validation attribute
                        attributes.Remove(attribute.Key);
                    }
                }
            }

            // Add any input type specific rules that weren't already added. 
            if (attributes.TryGetValue("type", out string type))
            {
                if (type != "text")
                {
                    if (_inputTypeAdapters.TryGetValue(type, out var handler))
                    {
                        handler(type, modelExplorer.Metadata, rules);
                    }
                }
            }

            // Merge the rules (including rules declared in the markup) into a single attribute.
            VueHtmlAttributeHelper.MergeVeeValidateAttributes(attributes, rules);
        }        
    }
}
