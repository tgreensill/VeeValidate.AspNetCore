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

				if (context.ModelMetadata.UnderlyingOrModelType == typeof(DateTime))
				{
					var dateFormat = _dateFormatProvider(context.ActionContext.HttpContext);

					if (!DateTime.TryParse(from, out var minDate))
					{
						throw new ArgumentException(nameof(rangeValidator.From));
					}

					if (!DateTime.TryParse(to, out var maxDate))
					{
						throw new ArgumentException(nameof(rangeValidator.To));
					}

					context
						.AddValidationRule("date_format", $"'{dateFormat}'")
						.AddValidationRule("date_between", $"['{minDate.ToString(dateFormat)}','{maxDate.ToString(dateFormat)}',true]");
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
