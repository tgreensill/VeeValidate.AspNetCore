using System;
using FluentValidation.AspNetCore;
using FluentValidation.Internal;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.FluentValidation.Adapters
{
	public class InclusiveBetweenClientValidator : ClientValidatorBase
	{
		private readonly Func<HttpContext, string> _dateFormatProvider;
		
		public InclusiveBetweenClientValidator(PropertyRule rule, IPropertyValidator validator, Func<HttpContext, string> dateFormatProvider) : base(rule, validator)
		{
			_dateFormatProvider = dateFormatProvider;
		}

		public override void AddValidation(ClientModelValidationContext context)
		{
			var rangeValidator = (InclusiveBetweenValidator) Validator;

			if (rangeValidator.To != null && rangeValidator.From != null)
			{
				var from = rangeValidator.From.ToString();
				var to = rangeValidator.To.ToString();

				context.AddValidationDisplayName();

				if (context.ModelMetadata.UnderlyingOrModelType == typeof(DateTime))
				{
					var dateFormat = _dateFormatProvider(context.ActionContext.HttpContext);
					var normalisedDateFormat = dateFormat.Replace('D', 'd').Replace('Y', 'y');

					context.AddValidationRule("date_format", $"'{dateFormat}'");

					if (DateTime.TryParse(from, out var minDate))
					{
						context.AddValidationRule("after", $"['{minDate.ToString(normalisedDateFormat)}',true]");
					}

					if (DateTime.TryParse(to, out var maxDate))
					{
						context.AddValidationRule("before", $"['{maxDate.ToString(normalisedDateFormat)}',true]");
					}
				}
				else
				{
					context
						.AddValidationRule("min_value", from)
						.AddValidationRule("max_value", to);
				}
			}
		}
	}
}
