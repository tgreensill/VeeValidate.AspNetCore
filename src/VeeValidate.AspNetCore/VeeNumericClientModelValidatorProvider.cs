using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using VeeValidate.AspNetCore.Adapters;

namespace VeeValidate.AspNetCore
{
    /// <summary>
    /// An implementation of <see cref="IClientModelValidatorProvider"/> which provides client validators
    /// for specific numeric types.
    /// </summary>
    public class VeeNumericClientModelValidatorProvider : IClientModelValidatorProvider
    {
        /// <inheritdoc />
        public void CreateValidators(ClientValidatorProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var typeToValidate = context.ModelMetadata.UnderlyingOrModelType;

            // Check only the numeric types for which we set type='text'.
            if (typeToValidate == typeof(float) ||
                typeToValidate == typeof(double) ||
                typeToValidate == typeof(decimal))
            {
                for (var i = 0; i < context.Results.Count; i++)
                {
                    var validator = context.Results[i].Validator;
                    if (validator != null && validator is DecimalClientValidator)
                    {
                        // A validator is already present. No need to add one.
                        return;
                    }
                }
                // decimals:true
                context.Results.Add(new ClientValidatorItem
                {
                    Validator = new DecimalClientValidator(),
                    IsReusable = true
                });
            }

            if (typeToValidate == typeof(int))
            {
                for (var i = 0; i < context.Results.Count; i++)
                {
                    var validator = context.Results[i].Validator;
                    if (validator != null && validator is NumericClientValidator)
                    {
                        // A validator is already present. No need to add one.
                        return;
                    }
                }
                // decimals:true
                context.Results.Add(new ClientValidatorItem
                {
                    Validator = new NumericClientValidator(),
                    IsReusable = true
                });
            }
        }
    }
}
