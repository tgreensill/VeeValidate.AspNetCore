using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace VeeValidate.AspNetCore.FluentValidation.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            string VeeValidateDateFormatProvider(HttpContext ctx) => "DD/MM/YYYY";

            // Add Vee Validation before AddMvc.
            services.AddVeeValidation(options =>
            {
                options.ValidationInputCssClassName = "invalid";
                options.ValidationMessageCssClassName = "red-text";
                options.DateFormatProvider = VeeValidateDateFormatProvider;
            });

            services
                .AddMvc()
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssembly(typeof(Startup).Assembly);
                    config.ConfigureClientsideValidation(options =>
                        options.UseVeeValidate(VeeValidateDateFormatProvider)
                    );
                });
        }

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
