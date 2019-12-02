using System;
using System.Net.WebSockets;
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
using System.Threading;
using System.Threading.Tasks;
using NGK_LAB10_WebAPI.Controllers;
using NGK_LAB10_WebAPI.Hubs;

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
            services.AddSingleton<IConfiguration>(Configuration);

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
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecret"])),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true
                           
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
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }


            #region WebSocket

            //app.UseWebSockets();
            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Path == "/Subscribe")
            //    {
            //        if (context.WebSockets.IsWebSocketRequest)
            //        {
            //            WebSocket w = await context.WebSockets.AcceptWebSocketAsync();
            //            await Subscribe(context, w);
            //        }
            //        else
            //            context.Response.StatusCode = 400;
            //    }
            //    else
            //        await next();
            //});

            #endregion

            #region SignalR

            

            #endregion


            app.UseHttpsRedirection();

            app.UseRouting();

            //Bruges ikke roller i dette projekt
            

            //commented, since we useMvc instead
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            //Adding for authentication

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSignalR(routes => { routes.MapHub<SubscribeHub>("/SubscribeWeather"); });

            app.UseAuthentication();
            app.UseAuthorization();

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

        //private async Task TSubscribe(HttpContext context, WebSocket webSocket)
        //{
        //    var buffer = new byte[1024 * 4];
        //    var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        //    while (!result.CloseStatus.HasValue)
        //    {
        //        await webSocket.SendAsync(new ArraySegment<byte>(buffer,0,result.Count ),
        //            result.MessageType, result.EndOfMessage,CancellationToken.None);

        //        result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        //    }

        //    await webSocket.CloseAsync(result.CloseStatus.Value, 
        //        result.CloseStatusDescription, CancellationToken.None);
        //}

        //private async Task Subscribe(HttpContext context, WebSocket webSocket)
        //{



        //    var buffer = new byte[1024 * 4];
        //    var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        //    while (!result.CloseStatus.HasValue)
        //    {
        //        await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count),
        //            result.MessageType, result.EndOfMessage, CancellationToken.None);

        //        result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        //    }

        //    await webSocket.CloseAsync(result.CloseStatus.Value,
        //        result.CloseStatusDescription, CancellationToken.None);
        //}
    }
}
