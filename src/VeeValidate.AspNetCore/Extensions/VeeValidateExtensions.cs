using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using VeeValidate.AspNetCore.ViewFeatures;

// ReSharper disable CheckNamespace
namespace VeeValidate.AspNetCore
{
    public static class VeeValidateExtensions
    {
        public static IServiceCollection AddVeeValidation(this IServiceCollection services, Action<VeeValidateOptions> optionsExpression = null)
        {
            var expr = optionsExpression ?? delegate { };
            var options = new VeeValidateOptions();

            expr(options);

            services.TryAddSingleton(options);
            services.TryAddSingleton<VeeValidateSnippets>();
            services.TryAddTransient<IValidationAttributeAdapterProvider, VeeValidateAttributeAdapterProvider>();
            
            if (options.OverrideValidationTagHelpers)
            {
                services.TryAddSingleton<IHtmlGenerator, VeeValidateHtmlGenerator>();
            }

            return services;
        }
    }
}
