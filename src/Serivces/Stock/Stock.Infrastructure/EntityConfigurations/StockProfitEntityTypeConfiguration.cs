
using Stock.Domain.AggregatesModel.StockAggregate;

namespace Stock.Infrastructure.EntityConfigurations
{
    /// <summary>
    /// Конфигурация для таблицы StockProfit
    /// </summary>
    public class StockProfitEntityTypeConfiguration : IEntityTypeConfiguration<StockProfit>
    {
        public void Configure(EntityTypeBuilder<StockProfit> builder)
        {
            builder.ToTable(nameof(StockProfit), StockContext.DEFAULT_SCHEMA);
            builder.HasKey(s => s.Id);
            builder.Ignore(b => b.DomainEvents);
            builder.Property(s => s.Id)
                   .UseHiLo("stockprofitseq", StockContext.DEFAULT_SCHEMA);
        }
    }
}
