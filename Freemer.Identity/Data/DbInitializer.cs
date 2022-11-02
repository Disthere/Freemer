using Freemer.Identity.Models;
using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;

namespace Freemer.Identity.Data
{
    public static class DatabaseInitializer
    {
        public static void Init(IServiceProvider scopeServiceProvider)
        {
            var userManager = scopeServiceProvider.GetService<UserManager<AppUser>>();

            var user = new AppUser
            {
                UserName = "User"
            };

            var result = userManager.CreateAsync(user, "123qwe").GetAwaiter().GetResult();
            if (result.Succeeded)
            {
                userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Administrator")).GetAwaiter().GetResult();
                userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Scope, "OrdersAPI")).GetAwaiter().GetResult();
            }

            //scopeServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            //var context = scopeServiceProvider.GetRequiredService<ConfigurationDbContext>();
            //context.Database.Migrate();
            //if (!context.Clients.Any())
            //{
            //    foreach (var client in IdentityServerConfiguration.GetClients())
            //    {
            //        context.Clients.Add(client.ToEntity());
            //    }
            //    context.SaveChanges();
            //}

            //if (!context.IdentityResources.Any())
            //{
            //    foreach (var resource in IdentityServerConfiguration.GetIdentityResources())
            //    {
            //        context.IdentityResources.Add(resource.ToEntity());
            //    }
            //    context.SaveChanges();
            //}

            //if (!context.ApiResources.Any())
            //{
            //    foreach (var resource in IdentityServerConfiguration.GetApiResources())
            //    {
            //        context.ApiResources.Add(resource.ToEntity());
            //    }
            //    context.SaveChanges();
            //}
        }
    }

    // Вместо инициализации используется стартовая миграция
    //public class DbInitializer
    //{
    //    public static void Initialize(IApplicationBuilder app)
    //    {
    //        using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
    //        {
    //            serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

    //            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
    //            context.Database.Migrate();
    //            if (!context.Clients.Any())
    //            {
    //                foreach (var client in IdentityServerConfiguration.Clients)
    //                {
    //                    context.Clients.Add(client.ToEntity());
    //                }
    //                context.SaveChanges();
    //            }

    //            if (!context.IdentityResources.Any())
    //            {
    //                foreach (var resource in IdentityServerConfiguration.IdentityResources)
    //                {
    //                    context.IdentityResources.Add(resource.ToEntity());
    //                }
    //                context.SaveChanges();
    //            }

    //            if (!context.ApiScopes.Any())
    //            {
    //                foreach (var resource in IdentityServerConfiguration.ApiScopes)
    //                {
    //                    context.ApiScopes.Add(resource.ToEntity());
    //                }
    //                context.SaveChanges();
    //            }
    //        }
    //    }
    //}
}
