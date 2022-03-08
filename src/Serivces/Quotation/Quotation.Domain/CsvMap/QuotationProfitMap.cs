using CsvHelper.Configuration;

using Quotation.Domain.AggregatesModel.QuotationProfitAggregate;

namespace Quotation.Domain.CsvMap
{
    public sealed class QuotationProfitMap : ClassMap<QuotationProfit>
    {
        public QuotationProfitMap()
        {
            Map(m => m.InvestedAmount);
            Map(m => m.CountBuyQuotationPossible);
            Map(m => m.PriceAvg);
            Map(m => m.QuantityPaymentsAvg);
            Map(m => m.PayoutAvg);
            Map(m => m.PayoutsYieldAvg);
            Map(m => m.PossibleProfitSpeculation);
        }
    }
}
