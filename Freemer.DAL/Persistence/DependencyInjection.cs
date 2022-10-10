using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Freemer.DAL.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSqliteDbConnection(this IServiceCollection
            services, IConfiguration configuration)
        {
            var connectionString = configuration["SqliteDbConnection"];

            services.AddDbContext<FreemerDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            services.AddScoped<IFreemerDbContext>(provider =>
            provider.GetService<FreemerDbContext>());

            return services;
        }

        public static IServiceCollection AddSqlServerDbConnection(this IServiceCollection
            services, IConfiguration configuration)
        {
            var connectionString = configuration["SqlServerDbConnection"];

            services.AddDbContext<FreemerDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IFreemerDbContext>(provider =>
            provider.GetService<FreemerDbContext>());

            return services;
        }
    }
}
