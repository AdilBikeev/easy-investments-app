using Microsoft.Extensions.Logging;

using Quotation.BuildingBlocks.Database;
using Quotation.BuildingBlocks.Database.Abstractions;
using QuotationAggregate = Quotation.Domain.AggregatesModel.QuotationAggregate;
using Quotation.Domain.CsvMap;

namespace Quotation.Infrastructure.Seeding
{
    [Obsolete("Подругзка из CSV файла уже не нужна. Информация по котировкам уже не является статичной")]
    public class QuotationDbSeeder : BaseCsvDbSeeder<QuotationContext>
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
            ImportEntitiesFromCsvFile<QuotationAggregate.Quotation, QuotationMap>(dbContext);
        }
    }
}
