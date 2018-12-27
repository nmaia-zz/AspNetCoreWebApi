using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Project.IoC;
using Project.WebApi.Helpers;
using Project.WebApi.Mapper;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace Project.WebApi
{
    /// <summary>
    /// Startup class of the solution. All the used Middlewares are here.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Dependency injection of configuration object
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// All the services we're using in the solution.
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">IServiceCollection services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.RegisterServices();

            var mappingConfig = new MapperConfiguration(mc => {

                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen(s => {

                s.SwaggerDoc("v1", new Info {

                    Version = "v1",
                    Title = "AspNetCore 2.1 WebAPI",                    
                    Description = "A WebAPI to talk about Star Wars.",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Natanael Maia",
                        Email = "gm.natanael@gmail.com",
                        Url = @"https://github.com/nmaia"
                    },
                    License = new License
                    {
                        Name = "MIT",
                        Url = @"https://github.com/angular/angular.js/blob/master/LICENSE"
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);
            });

            services.AddResponseCompression(options => {

                options.Providers.Add<BrotliCompressorProvider>();
                options.EnableForHttps = true;
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline. 
        /// </summary>
        /// <param name="app">IApplicationBuilder app</param>
        /// <param name="env">IHostingEnvironment env</param>
        /// <param name="loggerFactory">ILoggerFactory loggerFactory</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(s => {

                s.SwaggerEndpoint("/swagger/v1/swagger.json", "AspNetCore 2.1 WebAPI V1");
            });

            app.UseResponseCompression();

            app.UseMvc();
        }
    }
}
