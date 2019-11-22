using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CoreAPISample.API.Helpers;
using CoreAPISample.API.Models;
using CoreAPISample.Core.Common;
using CoreAPISample.Core.Logging;
using CoreAPISample.Core.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace CoreAPISample
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

            services.AddTransient<IMaterialsRepository, MaterialsRepository>();
            services.AddTransient<IDbContextFactory, DbContextFactory>();
            services.AddTransient<IDbContext, DbContext>();
            services.AddTransient<IMethodLoggerFactory, MethodLoggerFactory>();
            services.AddTransient<IMethodLogger, MethodLogger>();
            services.AddTransient<IAppHelper, AppHelper>();

            services.AddHttpClient<IHttpClientProvider, HttpClientProvider>();
            services.Configure<HttpConfiguartion>(Configuration.GetSection("HttpConfiguartion"));

            services.AddAutoMapper(typeof(Startup));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            Mapper.Initialize(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AddProfile<MappingsProfile>();
            });

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            services.AddSession();

           ////  ------------Jwt Authentication  ------------// 
           var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            ////  ------------Jwt Authentication Ends ------------// 

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseCors(builder =>
            builder.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()
            .SetPreflightMaxAge(TimeSpan.FromMinutes(10)));

            ////  ------------ Serilog Logger Configuration  ------------// 
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
            ////  ------------ Serilog Logger Configuration Ends ------------// 

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=MaterialsClient}/{action=Login}/{id?}");
            });
        }
    }
}
