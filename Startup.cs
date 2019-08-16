using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using stock_portfolio_server.Models;
using stock_portfolio_server.services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace stock_portfolio_server
{
    public class Startup
    {

        private readonly IConfiguration _configuration;
        private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://192.168.1.131:3000", "http://localhost:3000", "http://localhost:3001")
                                             .AllowAnyHeader()
                                             .AllowAnyMethod();
                    });
            });

            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<IdentityDbContext>(
                options => options.UseMySql(_configuration.GetConnectionString("localhost"),
                mySqlOptions =>
                {
                    mySqlOptions.ServerVersion(new Version(8, 8, 17), ServerType.MySql);
                    mySqlOptions.MigrationsAssembly(migrationAssembly);
                }
            ));

            services.AddIdentity<IdentityUser, IdentityRole>(options => { })
                    .AddEntityFrameworkStores<IdentityDbContext>();
            services.AddScoped<IUserStore<IdentityUser>, UserOnlyStore<IdentityUser, IdentityDbContext>>();
            services.AddMvc();
            services.AddHttpClient();
            services.AddAuthentication("cookies").AddCookie("cookies", options => options.LoginPath = "/generalauth/Login");
        }

        private void UserOnlyStore<T>()
        {
            throw new NotImplementedException();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseMvc(ConfigureRoutes);
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default",
                "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
