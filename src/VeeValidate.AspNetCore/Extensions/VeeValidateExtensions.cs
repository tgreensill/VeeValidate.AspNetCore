using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
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
            services.TryAddTransient<IValidationAttributeAdapterProvider, VeeAttributeAdapterProvider>();
            services.TryAddSingleton<IHtmlGenerator, VeeValidationHtmlGenerator>();

            services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<MvcViewOptions>, VeeValidateViewOptionsSetup>(s =>
            {
                return new VeeValidateViewOptionsSetup(options);
            }));

            return services;
        }

        internal class VeeValidateViewOptionsSetup : IConfigureOptions<MvcViewOptions>
        {
            private readonly VeeValidateOptions _configuration;

            public VeeValidateViewOptionsSetup(VeeValidateOptions configuration)
            {
                _configuration = configuration;
            }

            public void Configure(MvcViewOptions options)
            {
                options.ClientModelValidatorProviders.Add(new VeeNumericClientModelValidatorProvider(_configuration));                
            }
        }
    }
}
