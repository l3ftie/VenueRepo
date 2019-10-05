using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using VenueAPI.BLL;
using VenueAPI.DAL;
using VenueAPI.Services;
using VenueAPI.Services.LocationIq;
using VLibraries.APIAbstractions;
using VLibraries.CommonMiddleware;
using VLibraries.ExceptionHadler;
using VLibraries.ExceptionHandler;
using VLibraries.HttpClientWrapper;

namespace VenueAPI
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
            services.AddSingleton<IExceptionHandler, ExceptionHandler>()
                .AddSingleton<IHttpClientWrapper, HttpClientWrapper>()
                .AddSingleton<IVenueProvider, VenueProvider>()
                .AddSingleton<IVenueRepository, VenueRepository>()
                .AddSingleton<IVenueImageProvider, VenueImageProvider>()
                .AddSingleton<IVenueImageRepository, VenueImageRepository>()
                .AddSingleton<ISpaceProvider, SpaceProvider>()
                .AddSingleton<ISpaceRepository, SpaceRepository>()
                .AddSingleton<ISpaceImageProvider, SpaceImageProvider>()
                .AddSingleton<ISpaceImageRepository, SpaceImageRepository>()
                .AddSingleton<ILocationIqProxy, LocationIqProxy>()
                .AddSingleton<ILocationIqProvider, LocationIqProvider>()
                .AddSingleton<ILocationIqRepository, LocationIqRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            string xmlFilePath = Path.Combine(System.AppContext.BaseDirectory, "VenueAPI.xml");


            services.AddSwaggerGen(option =>
            {
                option.IncludeXmlComments(xmlFilePath);

                option.SwaggerDoc("v2", new Info
                {
                    Title = "Venue API",
                    Version = "v1.0",
                    Description = "There should be functional CRUD operations for Entities Venue and Space. CRD operations should function for VenueImage and SpaceImage Entities\n\n" +
                    "Order of Operation:\n" +
                    "1) Add a Venue\n" +
                    "2) Add either Venue images or Spaces to the Venue\n" +
                    "3) Once a Space has been added, add images to the Space\n\n"
                });
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //Logging
            loggerFactory.AddNLog();
            env.ConfigureNLog("nlog.config");

            app.ConfigureCustomExceptionMiddleware<VenueAPIExceptionMiddleware>();

            //app.UseHttpsRedirection();

            app.UseMvc();

            //Swagger API doc
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "Venue API");
            });
        }
    }
}
