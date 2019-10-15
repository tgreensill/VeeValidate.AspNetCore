using System;
using FluentValidation.AspNetCore;
using FluentValidation.Internal;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.FluentValidation.Adapters
{
    public class GreaterThanOrEqualClientValidator : ClientValidatorBase
    {
        private readonly Func<HttpContext, string> _dateFormatProvider;
        
        public GreaterThanOrEqualClientValidator(PropertyRule rule, IPropertyValidator validator, Func<HttpContext, string> dateFormatProvider) : base(rule, validator)
        {
            _dateFormatProvider = dateFormatProvider;
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var rangeValidator = (GreaterThanOrEqualValidator)Validator;

            if (rangeValidator.ValueToCompare != null)
            {
                if (context.ModelMetadata.UnderlyingOrModelType == typeof(DateTime))
                {
                    var dateFormat = _dateFormatProvider(context.ActionContext.HttpContext);

                    if (!DateTime.TryParse(rangeValidator.ValueToCompare.ToString(), out var minDate))
                    {
                        throw new ArgumentException(nameof(rangeValidator.ValueToCompare));
                    }

                    context
                        .AddValidationRule("date_format", $"'{dateFormat}'")
                        .AddValidationRule("date_between", $"['{minDate.ToString(dateFormat)}','{DateTime.MaxValue.ToString(dateFormat)}',true]");
                }
                else
                {
                    context.AddValidationRule("min_value", rangeValidator.ValueToCompare);
                }
            }
        }
    }
}
