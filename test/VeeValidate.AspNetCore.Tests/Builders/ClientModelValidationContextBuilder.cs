using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VeeValidate.AspNetCore.Tests.Builders
{
    public class ClientModelValidationContextBuilder
    {
        private ActionContext _actionContext;
        private ModelMetadata _modelMetadata;
        private IModelMetadataProvider _modelMetadataProvider;
        private IDictionary<string,string> _attributes;
        private Type _modelType;

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

        public ClientModelValidationContextBuilder WithModel(ValidationAttribute model)
        {
            return WithModelType(model.GetType());            
        }

        public ClientModelValidationContextBuilder WithModelType(Type modelType)
        {
            _modelType = modelType;
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
            if (_modelMetadata == null && _modelType == null)
            {
                throw new ArgumentNullException("ModelMetadata, Model, or Model Type must be set.");
            }

            var modelMetadata = _modelMetadata ?? _modelMetadataProvider.GetMetadataForType(_modelType);

            return new ClientModelValidationContext(_actionContext, modelMetadata, _modelMetadataProvider, _attributes);
        }
    }
}
