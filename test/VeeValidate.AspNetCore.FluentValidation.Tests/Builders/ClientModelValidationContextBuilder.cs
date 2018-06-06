using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace VeeValidate.AspNetCore.FluentValidation.Tests.Builders
{
    public class ClientModelValidationContextBuilder
    {
        private ActionContext _actionContext;
        private ModelMetadata _modelMetadata;
        private IModelMetadataProvider _modelMetadataProvider;
        private IDictionary<string, string> _attributes;
        
        public ClientModelValidationContextBuilder()
        {
            _actionContext = new ActionContext();
            _modelMetadataProvider = new EmptyModelMetadataProvider();
            _attributes = new AttributeDictionary();
        }

        public ClientModelValidationContextBuilder WithActionContext(ActionContext actionContext)
        {
            _actionContext = actionContext;
            return this;
        }
  
        public ClientModelValidationContextBuilder WithModelType<T>()
        {
            return WithModelType(typeof(T));
        }

        public ClientModelValidationContextBuilder WithModelType(Type type)
        {
            _modelMetadata = _modelMetadataProvider.GetMetadataForType(type);
            return this;
        }

        public ClientModelValidationContextBuilder WithModelMetaData(ModelMetadata modelMetadata)
        {
            _modelMetadata = modelMetadata;
            return this;
        }

        public ClientModelValidationContextBuilder WithModelMetadataProvider(IModelMetadataProvider modelMetadataProvider)
        {
            _modelMetadataProvider = modelMetadataProvider;
            return this;
        }

        public ClientModelValidationContextBuilder WithAttributes(IDictionary<string, string> attributes)
        {
            _attributes = attributes;
            return this;
        }

        public ClientModelValidationContextBuilder WithAttribute(KeyValuePair<string, string> attribute)
        {
            _attributes.Add(attribute);
            return this;
        }

        public ClientModelValidationContext Build()
        {
            if (_modelMetadata == null)
            {
                throw new ArgumentNullException(nameof(_modelMetadata));
            }

            return new ClientModelValidationContext(_actionContext, _modelMetadata, _modelMetadataProvider, _attributes);
        }
    }
}
