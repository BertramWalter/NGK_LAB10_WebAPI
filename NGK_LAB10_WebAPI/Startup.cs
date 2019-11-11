using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NGK_LAB10_WebAPI.Data;

namespace NGK_LAB10_WebAPI
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
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "WeatherObservation-Interval",
                    pattern:
                    "{controller=WeatherObservation}/{action=GetWeatherObservationBetweenIntervals}/{StartTime}/{EndTime}"
                    );
                endpoints.MapControllerRoute(
                    name: "WeatherObservation-ByDate",
                    pattern:
                    "{controller=WeatherObservation}/{action=GetWeatherByDate}/{Date}"
                );
                endpoints.MapControllerRoute(
                    name: "WeatherObservation-Latest",
                    pattern:
                    "{controller=WeatherObservation}/{action=GetLatestWeatherData}"
                );
            });
        }
    }
}
