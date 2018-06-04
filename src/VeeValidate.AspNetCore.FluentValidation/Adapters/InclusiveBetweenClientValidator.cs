using System;
using FluentValidation.Internal;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.FluentValidation.Adapters
{
    public class InclusiveBetweenClientValidator : VeeValidateClientValidatorBase
    {
        private readonly VeeValidateOptions _options;

        public InclusiveBetweenClientValidator(PropertyRule rule, IPropertyValidator validator, VeeValidateOptions options) : base(rule, validator)
        {
            _options = options;
        }

		public override void AddValidation(ClientModelValidationContext context)
		{
            var rangeValidator = (InclusiveBetweenValidator) Validator;

		    if (rangeValidator.To != null && rangeValidator.From != null)
		    {
		        var from = rangeValidator.From.ToString();
		        var to = rangeValidator.To.ToString();

                MergeAttribute(context.Attributes, "data-vv-as", context.ModelMetadata.GetDisplayName());

                if (context.ModelMetadata.UnderlyingOrModelType == typeof(DateTime))
                {
                    var normalisedDateFormat = _options.Dates.Format.Replace('D', 'd').Replace('Y', 'y');

                    MergeValidationAttribute(context.Attributes, "date_format", $"'{_options.Dates.Format}'");

                    if (DateTime.TryParse(from, out var minDate))
                    {
                        MergeValidationAttribute(context.Attributes, "after", $"['{minDate.ToString(normalisedDateFormat)}',true]");
                    }

                    if (DateTime.TryParse(to, out var maxDate))
                    {
                        MergeValidationAttribute(context.Attributes, "before", $"['{maxDate.ToString(normalisedDateFormat)}',true]");
                    }
                }
                else
                {
                    MergeValidationAttribute(context.Attributes, "min_value", from);
                    MergeValidationAttribute(context.Attributes, "max_value", to);
                }
			}
		}
	}
}
