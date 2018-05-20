using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using VeeValidate.AspNetCore.Adapters;
using VeeValidate.AspNetCore.ViewFeatures;

// ReSharper disable CheckNamespace
namespace VeeValidate.AspNetCore
{
    public static class VeeValidateExtensions
    {
        /// <summary>
        /// Wire up dependency injection for Vee Validation.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="optionsExpression"></param>
        /// <returns></returns>
        public static IServiceCollection AddVeeValidation(this IServiceCollection services, Action<VeeValidateOptions> optionsExpression = null)
        {
            var expr = optionsExpression ?? delegate { };
            var options = new VeeValidateOptions();

            expr(options);

            services.TryAddSingleton(options);
            services.TryAddSingleton<VeeValidateSnippets>();
            services.TryAddSingleton<IHtmlGenerator, VeeValidateHtmlGenerator>();
            services.TryAddSingleton<ValidationHtmlAttributeProvider, VeeValidateHtmlAttributeProvider>();

            // Add adapters
            services.AddTransient<IHtmlValidationAttributeAdapter, CompareAttributeAdapter>();
            services.AddTransient<IHtmlValidationAttributeAdapter, CreditCardAttributeAdapter>();
            services.AddTransient<IHtmlValidationAttributeAdapter, DateAttributeAdapter>();
            services.AddTransient<IHtmlValidationAttributeAdapter, EmailAddressAttributeAdapter>();
            services.AddTransient<IHtmlValidationAttributeAdapter, FileExtensionsAttributeAdapter>();
            services.AddTransient<IHtmlValidationAttributeAdapter, MaxLengthAttributeAdapter>();
            services.AddTransient<IHtmlValidationAttributeAdapter, MinLengthAttributeAdapter>();
            services.AddTransient<IHtmlValidationAttributeAdapter, NumberAttributeAdapter>();
            services.AddTransient<IHtmlValidationAttributeAdapter, RangeMaxAttributeAdapter>();
            services.AddTransient<IHtmlValidationAttributeAdapter, RangeMinAttributeAdapter>();
            services.AddTransient<IHtmlValidationAttributeAdapter, RegularExpressionAttributeAdapter>();
            services.AddTransient<IHtmlValidationAttributeAdapter, RequiredAttributeAdapter>();
            services.AddTransient<IHtmlValidationAttributeAdapter, UrlAttributeAdapter>();

            return services;
        }
    }
}
