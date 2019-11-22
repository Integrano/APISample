using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreAPISample.Core.Common;
using CoreAPISample.Core.Repository;
using CoreAPISample.UI.Helpers;
using CoreAPISample.UI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CoreAPISample.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            CoreAPISampleDBContext.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddHttpClient<IHttpClientProvider, HttpClientProvider>();

            services.Configure<HttpConfiguartion>(Configuration.GetSection("HttpConfiguartion"));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddTransient<IMaterialsRepository, MaterialsRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<FormOptions>(options => {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = int.MaxValue;
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseCors(builder =>
            builder.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()
            .SetPreflightMaxAge(TimeSpan.FromMinutes(10)));

            loggerFactory.AddSerilog();

            var logFilePath = $"{Configuration.GetSection("LogFilePath").Value}{DateTime.Today:yyyyMMdd}.log";
            var loggerConfiguration = new SerilogLoggerConfiguration();
            Configuration.GetSection("SerilogLoggerConfiguration").Bind(loggerConfiguration);

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentUserName()
                .WriteTo.File(logFilePath,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} {EnvironmentUserName} [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}",
                    fileSizeLimitBytes: null,
                    retainedFileCountLimit: null,
                    shared: true,
                    restrictedToMinimumLevel: loggerConfiguration.LogLevel)
                .CreateLogger();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=MaterialsClient}/{action=Login}/{id?}");
            });
        }
    }
}
