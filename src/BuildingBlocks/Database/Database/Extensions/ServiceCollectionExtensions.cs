using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Quotation.BuildingBlocks.Database.Abstractions;

namespace Quotation.BuildingBlocks.Database.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterDbContext<TContext, TMigrationHistoryRepository>(this IServiceCollection services, IConfiguration configuration, string connectionStringName)
            where TContext : DbContext, IMigratoryDbContext
            where TMigrationHistoryRepository : IHistoryRepository
        {
            var assemblyName = typeof(TContext).Assembly.FullName;
            var connectionString = configuration.GetConnectionString(connectionStringName);
            services.AddDbContext<TContext>(options =>
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly(assemblyName))
                    .ReplaceService<IHistoryRepository, TMigrationHistoryRepository>(),
                    ServiceLifetime.Scoped);

            services.AddTransient(typeof(IMigratoryDbContext), typeof(TContext));

            services.TryAddScoped<DatabaseMigrator>();
        }

        public static void RegisterDbContextFactory<TContext, TMigrationHistoryRepository>(this IServiceCollection services, IConfiguration configuration, string connectionStringName)
            where TContext : DbContext, IMigratoryDbContext
            where TMigrationHistoryRepository : IHistoryRepository
        {
            var assemblyName = typeof(TContext).Assembly.FullName;
            var connectionString = configuration.GetConnectionString(connectionStringName);

            services.AddDbContextFactory<TContext>(options =>
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly(assemblyName))
                    .ReplaceService<IHistoryRepository, TMigrationHistoryRepository>());


            services.AddTransient(typeof(IMigratoryDbContext), typeof(TContext));

            services.TryAddScoped<DatabaseMigrator>();
        }
    }
}
