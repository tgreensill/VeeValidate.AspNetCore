using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VeeValidate.AspNetCore.Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string DateFormatProvider(HttpContext ctx) => "DD/MM/YYYY";

            // Add Vee Validation before MVC
            services.AddVeeValidation(options =>
            {
                options.ValidationInputCssClassName = "invalid";
                options.ValidationMessageCssClassName = "red-text";
                options.DateFormatProvider = DateFormatProvider;
            });
            services.AddMvc()
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssembly(typeof(Startup).Assembly);
                    config.ConfigureClientsideValidation(options =>
                        options.UseVeeValidate(DateFormatProvider)
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
