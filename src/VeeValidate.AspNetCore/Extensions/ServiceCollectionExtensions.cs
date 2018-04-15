using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using VeeValidate.AspNetCore.ViewFeatures;

// ReSharper disable CheckNamespace
namespace VeeValidate.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddVeeValidation(this IServiceCollection services, Action<VeeValidateOptions> optionsExpression = null)
        {
            var expr = optionsExpression ?? delegate { };
            var options = new VeeValidateOptions();

            expr(options);

            services.TryAddSingleton(options);
            services.TryAddTransient<IValidationAttributeAdapterProvider, VeeValidationAttributeAdapterProvider>();
            services.TryAddSingleton<IHtmlGenerator, VeeValidationHtmlGenerator>();

            return services;
        }
    }
}
