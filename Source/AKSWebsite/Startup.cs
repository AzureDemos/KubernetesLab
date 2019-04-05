using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AKSWebsite.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AKSWebsite
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(env.ContentRootPath)
             .AddJsonFile("Configs/appsettings.json", optional: false, reloadOnChange: true)
             .AddJsonFile($"Configs/Environments/{env.EnvironmentName}/Json/configmap-website.json", optional: true)
             .AddXmlFile($"Configs/Environments/{env.EnvironmentName}/XML/configmap-website.xml", optional: true, reloadOnChange: true)
             .AddJsonFile($"Configs/Environments/{env.EnvironmentName}/Secrets/secret-website.json", optional: true)
             .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.All;
                //- https:/microsoft-my.sharepoint.com/:p:/p/manaccar/EXaCrgJdAOBFov_c0O_0KLwBhGalmbce7xP-1LAUYFwfqg?e=KIra1a
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddTransient(x => Configuration);
            services.AddTransient<IAPIService, APIService>();
            if (String.IsNullOrWhiteSpace(Configuration["Routing"]) || Configuration["Routing"].ToLower() == "env") 
                services.AddTransient<IServiceLocator, ServiceLocator>();
            else
                services.AddTransient<IServiceLocator, DNSServiceLocator>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseForwardedHeaders();
            app.UseDeveloperExceptionPage(); //We are leaving this on for this demo to help trace any errors
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
