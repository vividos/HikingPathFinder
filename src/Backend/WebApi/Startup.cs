using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

namespace HikingPathFinder.Backend.WebApi
{
    /// <summary>
    /// Startup class for the Web API
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup method
        /// </summary>
        /// <param name="env">hosting environment</param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        /// <summary>
        /// Configuration access
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">service collection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddSwaggerGen(this.ConfigureSwaggerGen);
        }

        /// <summary>
        /// Configures swagger generation; see
        /// https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger
        /// </summary>
        /// <param name="options">swagger generation options</param>
        private void ConfigureSwaggerGen(SwaggerGenOptions options)
        {
            options.SwaggerDoc(
                "v1",
                new Info
                {
                    Version = "v1",
                    Title = "HikingPathFinder API",
                    Contact = new Contact
                    {
                        Name = "Michael Fink",
                        Email = string.Empty,
                        Url = "https://github.com/vividos/HikingPathFinder"
                    },
                    License = new License
                    {
                        Name = "Creative Commons Attribution-ShareAlike 4.0 International License (CC-BY-SA)",
                        Url = "https://creativecommons.org/licenses/by-sa/4.0/"
                    }
                });

            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            var xmlPath = Path.Combine(basePath, "HikingPathFinder.Backend.WebApi.xml");
            options.IncludeXmlComments(xmlPath);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">application builder</param>
        /// <param name="env">hosting environment</param>
        /// <param name="loggerFactory">logger factory</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HikingPathFinder API v1");
            });
        }
    }
}
