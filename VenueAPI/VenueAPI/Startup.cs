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
using VLibraries.CommonMiddleware;
using VLibraries.ExceptionHadler;
using VLibraries.ExceptionHandler;

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
            services.AddSingleton<IVenueProvider, VenueProvider>()
                .AddSingleton<IVenueRepository, VenueRepository>()
                .AddSingleton<IExceptionHandler, ExceptionHandler>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            string xmlFilePath = Path.Combine(System.AppContext.BaseDirectory, "VenueAPI.xml");


            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v2", new Info { Title = "Venue API", Version = "v1.0" });
                option.IncludeXmlComments(xmlFilePath);
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

            app.UseHttpsRedirection();

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
