using System;
using System.Buffers;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Nop.Web.ConsultantTasks;
using Nop.Web.Framework.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;

namespace Nop.Web
{
    /// <summary>
    /// Represents startup class of application
    /// </summary>
    public class Startup
    {
        #region Properties

        /// <summary>
        /// Get configuration root of the application
        /// </summary>
        public IConfigurationRoot Configuration { get; }
        public IHaragPostPostsTracking HaragPostPostsTracking { get; }
        public IClosePostAfter48Hours ClosePostAfter48Hours { get; }

        #endregion

        #region Ctor

        public Startup(IHostingEnvironment environment)
        {

            //create configuration
            Configuration = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        #endregion

        /// <summary>
        /// Add services to the application and configure service provider
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            

            services.AddSingleton<Microsoft.AspNetCore.Mvc.Infrastructure.IActionContextAccessor, Microsoft.AspNetCore.Mvc.Infrastructure.ActionContextAccessor>();

            services.AddScoped<Microsoft.AspNetCore.Mvc.IUrlHelper>(factory =>
            {
                var actionContext = factory.GetService<Microsoft.AspNetCore.Mvc.Infrastructure.IActionContextAccessor>()
                                           .ActionContext;
                return new Microsoft.AspNetCore.Mvc.Routing.UrlHelper(actionContext);
            });

            //services.AddCors();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });

            //services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            //{
            //    builder.AllowAnyOrigin()
            //           .AllowAnyMethod()
            //           .AllowAnyHeader()
            //           .AllowCredentials();
            //}));

            //services.Configure<MvcOptions>(options =>
            //{
            //    options.Filters.Add(new CorsAuthorizationFilterFactory("MyPolicy"));
            //});
             
            services.AddTransient<HaragPostPostsTracking>();
            services.AddTransient<ClosePostAfter48Hours>();

            //services.AddTransient<ClosePostAfter48Hours>();
            return services.ConfigureApplicationServices(Configuration);
        }

        /// <summary>
        /// Configure the application HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application, IHaragPostPostsTracking HaragPostPostsTracking, IClosePostAfter48Hours ClosePostAfter48Hours)
        {
            //application.UseCors("MyPolicy");

            //application.UseCors(builder => builder.WithOrigins("http://localhost:4200")
            //                    .AllowAnyMethod()
            //                    .AllowAnyHeader()
            //                    .AllowCredentials());

            //application.UseCors(m => m.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());  



            application.UseCors("AllowAll");
            application.ConfigureRequestPipeline();
 
            ClosePostAfter48Hours.StartClosingService();
            HaragPostPostsTracking.StartPostDeletingService();
            HaragPostPostsTracking.RefreashPostRequestService();

        }
    }
}
