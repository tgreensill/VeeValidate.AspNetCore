using System;
using System.Collections.Generic;
using FluentValidation.AspNetCore;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using VeeValidate.AspNetCore.FluentValidation.Adapters;

namespace VeeValidate.AspNetCore.FluentValidation.Sample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Vee Validation before MVC
            services.AddVeeValidation(options =>
            {
                options.Dates.Format = "DD/MM/YYYY";
                options.ValidationInputCssClassName = "invalid";
                options.ValidationMessageCssClassName = "red-text";
            });

            services.AddMvc()
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssembly(typeof(Startup).Assembly);
                    config.ConfigureClientsideValidation(options =>
                        options.UseVeeValidate(new VeeValidateOptions
                        {
                            Dates = new DateValidationOptions {Format = "DD/MM/YYYY"}
                        }));
                });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
