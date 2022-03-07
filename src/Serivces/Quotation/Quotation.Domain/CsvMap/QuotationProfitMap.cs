using CsvHelper.Configuration;

using Quotation.Domain.AggregatesModel.QuotationAggregate;

namespace Quotation.Domain.CsvMap
{
    public sealed class QuotationProfitMap : ClassMap<QuotationProfit>
    {
        public QuotationProfitMap()
        {
            Map(m => m.FullName);
            Map(m => m.FIGI);
            Map(m => m.Ticker);
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
