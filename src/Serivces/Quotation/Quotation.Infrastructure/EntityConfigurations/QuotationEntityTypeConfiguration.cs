
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
            builder.Ignore(b => b.DomainEvents);
            builder.Property(s => s.Id)
                   .UseHiLo("quotationseq", QuotationContext.DEFAULT_SCHEMA);
        }
    }
}
