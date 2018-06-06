using FluentValidation.Validators;
using VeeValidate.AspNetCore;
using VeeValidate.AspNetCore.FluentValidation.Adapters;

// ReSharper disable CheckNamespace
namespace FluentValidation.AspNetCore
{
    public static class FluentValidationExtensions
    {
        public static void UseVeeValidate(this FluentValidationClientModelValidatorProvider config, VeeValidateOptions options)
        {
            config.ClientValidatorFactories[typeof(INotNullValidator)] = 
                (context, rule, validator) => new NotEmptyClientValidator(rule, validator);

            config.ClientValidatorFactories[typeof(INotEmptyValidator)] = 
                (context, rule, validator) => new NotEmptyClientValidator(rule, validator);

            config.ClientValidatorFactories[typeof(IEmailValidator)] = 
                (context, rule, validator) => new EmailClientValidator(rule, validator);

            config.ClientValidatorFactories[typeof(IRegularExpressionValidator)] = 
                (context, rule, validator) => new RegularExpressionClientValidator(rule, validator);

            config.ClientValidatorFactories[typeof(MaximumLengthValidator)] = 
                (context, rule, validator) => new MaximumLengthClientValidator(rule, validator);

            config.ClientValidatorFactories[typeof(MinimumLengthValidator)] = 
                (context, rule, validator) => new MinimumLengthClientValidator(rule, validator);

            config.ClientValidatorFactories[typeof(LengthValidator)] = 
                (context, rule, validator) => new LengthClientValidator(rule, validator);

            config.ClientValidatorFactories[typeof(ExactLengthValidator)] = 
                (context, rule, validator) => new LengthClientValidator(rule, validator);

            config.ClientValidatorFactories[typeof(InclusiveBetweenValidator)] = 
                (context, rule, validator) => new InclusiveBetweenClientValidator(rule, validator, options);

            config.ClientValidatorFactories[typeof(GreaterThanOrEqualValidator)] = (context, rule, validator) =>
                new GreaterThanOrEqualClientValidator(rule, validator, options);

            config.ClientValidatorFactories[typeof(LessThanOrEqualValidator)] = (context, rule, validator) =>
                new LessThanOrEqualClientValidator(rule, validator, options);

            config.ClientValidatorFactories[typeof(EqualValidator)] = 
                (context, rule, validator) => new EqualClientValidator(rule, validator);

            config.ClientValidatorFactories[typeof(CreditCardValidator)] = 
                (context, rule, validator) => new CreditCardClientValidator(rule, validator);
        }
    }
}
