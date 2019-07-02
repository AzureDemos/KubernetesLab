using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AKSWebsite.Services;
using k8s;
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
             .AddJsonFile($"Configs/Environments/{env.EnvironmentName}/Json/configmap-website.json", optional: true, reloadOnChange: true)
             .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                //options.ForwardedForHeaderName = "X-Original-URI";
                //options.OriginalForHeaderName = "X-Original-URI";
                //options.OriginalHostHeaderName = "X-Original-URI";
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddTransient(x => Configuration);
            services.AddTransient<IAPIService, APIService>();
            services.AddTransient<IServiceLocator, DNSServiceLocator>();

            if (this.Configuration["ASPNETCORE_ENVIRONMENT"] == "Development")
                services.AddSingleton<IKubernetes>(new Kubernetes(KubernetesClientConfiguration.BuildConfigFromConfigFile()));
            else
                services.AddSingleton<IKubernetes>(new Kubernetes(KubernetesClientConfiguration.InClusterConfig()));

            services.AddCookieTempData();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
           
           // app.UsePathBase("/web"); // DON'T FORGET THE LEADING SLASH!

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                //ForwardedForHeaderName = "X-Original-URI",
                //OriginalForHeaderName = "X-Original-URI",
                //OriginalHostHeaderName = "X-Original-URI",
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });


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
