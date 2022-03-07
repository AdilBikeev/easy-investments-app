using Microsoft.Extensions.Logging;

using Quotation.BuildingBlocks.Database;
using Quotation.BuildingBlocks.Database.Abstractions;
using QuotationAggregate = Quotation.Domain.AggregatesModel.QuotationAggregate;
using Quotation.Domain.CsvMap;

namespace Quotation.Infrastructure.Seeding
{
    internal class QuotationDbSeeder : BaseCsvDbSeeder<QuotationContext>
    {
        public QuotationDbSeeder(
            IDbContextFactory<QuotationContext> dbContextFactory,
            ILogger<BaseCsvDbSeeder<QuotationContext>> logger,
            IFileSystemAccessor fileAccessor) : base(dbContextFactory, logger, fileAccessor)
        {

        }

        /// <summary>
        /// Подгружает данные в БД Quotation.
        /// </summary>
        public override void SeedData(QuotationContext dbContext)
        {
            //ImportEntitiesFromCsvFile<QuotationProfit, QuotationProfitMap, int>(dbContext);
            ImportEntitiesFromCsvFile<QuotationAggregate.Quotation, QuotationMap, int>(dbContext);
        }
    }
}
