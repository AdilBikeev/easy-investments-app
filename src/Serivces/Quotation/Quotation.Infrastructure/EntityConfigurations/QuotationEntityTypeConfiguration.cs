
using Quotation.Domain.AggregatesModel.QuotationProfitAggregate;

using QuotationAggregate = Quotation.Domain.AggregatesModel.QuotationAggregate;

namespace Quotation.Infrastructure.EntityConfigurations
{
    /// <summary>
    /// Конфигурация для таблицы Quotation
    /// </summary>
    public class QuotationEntityTypeConfiguration : IEntityTypeConfiguration<QuotationAggregate.Quotation>
    {
        public void Configure(EntityTypeBuilder<QuotationAggregate.Quotation> builder)
        {
            builder.ToTable(nameof(QuotationAggregate.Quotation), QuotationContext.DEFAULT_SCHEMA);
            builder.HasKey(s => s.Id);
            builder.HasIndex(s => new { s.Name, s.FIGI, s.Ticker });
            builder.Ignore(b => b.DomainEvents);
            builder.Property(s => s.Id)
                   .UseHiLo("quotationseq", QuotationContext.DEFAULT_SCHEMA);

            builder
                .HasOne<QuotationProfit>(p => p.QuotationProfit)
                .WithOne(q => q.Quotation)
                .HasForeignKey<QuotationProfit>(p => p.QuotationId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
