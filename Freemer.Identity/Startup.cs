using Freemer.Identity.Data;
using Freemer.Identity.Models;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.Test;
using IdentityServer4.AspNetIdentity;
using Freemer.Identity.Infrastructure;

namespace Freemer.Identity
{
    public class Startup
    {
        private IConfiguration AppConfiguration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment) =>
           (AppConfiguration, Environment) = (configuration, environment);


        public void ConfigureServices(IServiceCollection services)
        {
                    
            string applicationDbConnectionString = AppConfiguration.GetValue<string>("ApplicationDbConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(applicationDbConnectionString));

            services.AddIdentity<AppUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 6;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireDigit = false;
                config.Password.RequireUppercase = false;
                config.User.RequireUniqueEmail = true;
                //config.SignIn.RequireConfirmedEmail = true;
                //config.SignIn.RequireConfirmedPhoneNumber = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "IdentityServer.Cookies";
                //config.Cookie.Name = "Freemer.Identity.Cookie";
                //config.LoginPath = "/Auth/Login";
                //config.LogoutPath = "/Auth/Logout";
            });

            string identityServerDbConnectionString = AppConfiguration.GetValue<string>("IdentityServerDbConnection");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityServer()
                .AddConfigurationStore(options =>
                {
                options.ConfigureDbContext = b => b.UseSqlServer(identityServerDbConnectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                options.ConfigureDbContext = b => b.UseSqlServer(identityServerDbConnectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddTestUsers(TestUsers.Users)
                
                //.AddProfileService<ProfileService>()
                .AddAspNetIdentity<AppUser>()
                .AddDeveloperSigningCredential();

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

#if DEBUG
            //DbInitializer.Initialize(app); 
#endif

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
