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
            services.AddCors();
            services.AddTransient<ClosePostAfter48Hours>();
            return services.ConfigureApplicationServices(Configuration);
        }

        /// <summary>
        /// Configure the application HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application, IClosePostAfter48Hours closePostAfter48Hours)
        {
            application.UseCors(m => m.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            application.ConfigureRequestPipeline();
            closePostAfter48Hours.StartClosingService();
        }
    }
}
