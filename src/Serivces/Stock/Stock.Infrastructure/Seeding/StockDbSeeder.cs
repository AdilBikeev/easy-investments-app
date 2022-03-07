using Microsoft.Extensions.Logging;

using Stock.BuildingBlocks.Database;
using Stock.BuildingBlocks.Database.Abstractions;
using Stock.Domain.AggregatesModel.StockAggregate;
using Stock.Domain.CsvMap;

namespace Stock.Infrastructure.Seeding
{
    internal class StockDbSeeder : BaseCsvDbSeeder<StockContext>
    {
        public StockDbSeeder(
            IDbContextFactory<StockContext> dbContextFactory,
            ILogger<BaseCsvDbSeeder<StockContext>> logger,
            IFileSystemAccessor fileAccessor) : base(dbContextFactory, logger, fileAccessor)
        {

        }

        /// <summary>
        /// Подгружает данные в БД Stock.
        /// </summary>
        public override void SeedData(StockContext dbContext)
        {
            ImportEntitiesFromCsvFile<StockProfit, StockProfitMap, int>(dbContext);
        }
    }
}
