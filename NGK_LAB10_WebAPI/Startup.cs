using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NGK_LAB10_WebAPI.Data;
using System.Text;

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

            //NEW
            services.AddMvc(mvcOptions => mvcOptions.EnableEndpointRouting = false);

            //Vi bruger bcrypt, så dette bruges ikke.
            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddEntityFrameworkStores<AppDbContext>();

            // Add authentication with JWT support
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecret"]))
                        };
                    });

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

            //Bruges ikke roller i dette projekt
            //app.UseAuthorization();

            //commented, since we useMvc instead
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            //Adding for authentication

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute("default","{controller=Home}/{action=Index}/{id?}");
            //});

            app.Run(async (context) =>
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Page not found");
            });

        }
    }
}
