using CsvHelper.Configuration;

namespace Quotation.Domain.CsvMap
{
    public sealed class QuotationMap : ClassMap<AggregatesModel.QuotationAggregate.Quotation>
    {
        public QuotationMap()
        {
            Map(m => m.Name);
            Map(m => m.FIGI);
            Map(m => m.Ticker);
        }
    }
}
