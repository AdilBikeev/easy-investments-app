
using Quotation.Domain.AggregatesModel.QuotationProfitAggregate;
using QuotationAggregate = Quotation.Domain.AggregatesModel.QuotationAggregate;

namespace Quotation.Infrastructure.EntityConfigurations
{
    /// <summary>
    /// Конфигурация для таблицы QuotationProfit
    /// </summary>
    public class QuotationProfitEntityTypeConfiguration : IEntityTypeConfiguration<QuotationProfit>
    {
        public void Configure(EntityTypeBuilder<QuotationProfit> builder)
        {
            builder.ToTable(nameof(QuotationProfit), QuotationContext.DEFAULT_SCHEMA);
            builder.HasKey(s => s.Id);
            builder.Ignore(b => b.DomainEvents);
            builder.Property(s => s.Id)
                   .UseHiLo("quotationprofitseq", QuotationContext.DEFAULT_SCHEMA);

            //builder
            //    .HasOne<QuotationAggregate.Quotation>(p => p.Quotation)
            //    .WithOne(q => q.QuotationProfit)
            //    .HasForeignKey<QuotationAggregate.Quotation>(p => p.QuotationId)
            //    .OnDelete(DeleteBehavior.NoAction)
            //    .IsRequired();
        }
    }
}
