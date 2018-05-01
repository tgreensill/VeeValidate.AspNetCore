using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            // TODO - Add Localization 
            //services.AddLocalization(); 
            services.AddVeeValidation(options => options.Dates.DateFormat = "DD/MM/YYYY");
            services.AddMvc();
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

            // TODO - Localization
            //var supportedCultures = new[]
            //{
            //    new CultureInfo("en-US"),
            //    new CultureInfo("en-GB"),
            //    new CultureInfo("en-NZ"),
            //    new CultureInfo("en"),                
            //    new CultureInfo("pt-BR"),
            //    new CultureInfo("pt"),
            //};

            //app.UseRequestLocalization(new RequestLocalizationOptions
            //{
            //    DefaultRequestCulture = new RequestCulture("en-NZ"),
            //    // Formatting numbers, dates, etc.
            //    SupportedCultures = supportedCultures,
            //    // UI strings that we have localized.
            //    SupportedUICultures = supportedCultures
            //});
            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
