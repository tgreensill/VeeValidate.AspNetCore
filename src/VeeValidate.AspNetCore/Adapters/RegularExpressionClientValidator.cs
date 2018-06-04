﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class RegularExpressionClientValidator : VeeValidateAttributeAdapter<RegularExpressionAttribute>
    {
        public RegularExpressionClientValidator(RegularExpressionAttribute attribute) : base(attribute)
        {
        }
        
        public override void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-vv-as", context.ModelMetadata.GetDisplayName());
            // Ensure the pattern starts and ends with '/'
            MergeValidationAttribute(context.Attributes, "regex", $"/{Attribute.Pattern.Trim('/')}/");
        }
    }
}
