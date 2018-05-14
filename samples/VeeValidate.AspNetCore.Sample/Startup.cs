using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
            // Add Vee Validation
            services.AddVeeValidation(options => options.Dates.Format = "DD/MM/YYYY");

            //https://andrewlock.net/adding-localisation-to-an-asp-net-core-application/
            //// Add Localization
            //services.Configure<RequestLocalizationOptions>(opts => {
            //    var supportedCultures = new CultureInfo[]
            //    {
            //        new CultureInfo("en-GB"),
            //        new CultureInfo("en-US"),
            //        new CultureInfo("en"),
            //        new CultureInfo("pt-BR"),
            //        new CultureInfo("pt"),
            //    };

            //    opts.DefaultRequestCulture = new RequestCulture("en-GB");
            //    // Formatting numbers, dates, etc.
            //    opts.SupportedCultures = supportedCultures;
            //    // UI strings that we have localized.
            //    opts.SupportedUICultures = supportedCultures;
            //});
            //services.AddLocalization(opts => {
            //    opts.ResourcesPath = "Resources"; }
            //);

            services.AddMvc();
                //.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, opts => {
                //    opts.ResourcesPath = "Resources";
                //})
                //.AddDataAnnotationsLocalization();
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

            // Add Localization Middleware
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            
            app.UseMvc();
        }
    }
}
