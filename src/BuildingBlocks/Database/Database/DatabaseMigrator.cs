using Quotation.BuildingBlocks.Database.Abstractions;
using Quotation.BuildingBlocks.Database.Extensions;

namespace Quotation.BuildingBlocks.Database
{
    /// <summary>
    /// Производит миграцию для всех БД. БД должны быть зарегистрированы с помощью
    /// <see cref="ServiceCollectionExtensions.RegisterDbContext{TContext,TMigrationHistoryRepository}"/>
    /// или <see cref="ServiceCollectionExtensions.RegisterDbContextFactory{TContext,TMigrationHistoryRepository}"/>
    /// </summary>
    public class DatabaseMigrator
    {
        private readonly IEnumerable<IMigratoryDbContext> _dbContexts;
        private readonly IEnumerable<IDbSeeder> _dbSeeders;
        private readonly ILogger<DatabaseMigrator> _logger;

        public DatabaseMigrator(
            IEnumerable<IMigratoryDbContext> dbContexts,
            ILogger<DatabaseMigrator> logger,
            IEnumerable<IDbSeeder> dbSeeders)
        {
            _dbContexts = dbContexts;
            _logger = logger;
            _dbSeeders = dbSeeders;
        }

        /// <summary>
        /// Произвоидт миграцию для всех БД.
        /// </summary>
        public void Migrate()
        {
            foreach (var dbContext in _dbContexts)
            {
                _logger.LogInformation($"{dbContext.GetType().Name} migration started...");
                dbContext.Migrate();
                _logger.LogInformation($"{dbContext.GetType().Name} migration completed.");
            }

            foreach (var dbSeeder in _dbSeeders)
            {
                _logger.LogInformation($"{dbSeeder.GetType().Name} data seeding started...");
                dbSeeder.InternalSeedData();
                _logger.LogInformation($"{dbSeeder.GetType().Name} data seeding completed.");
            }
        }
    }
}
