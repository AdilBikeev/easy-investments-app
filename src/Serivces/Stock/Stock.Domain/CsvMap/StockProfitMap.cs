using CsvHelper.Configuration;

using Stock.Domain.AggregatesModel.StockAggregate;

namespace Stock.Domain.CsvMap
{
    public sealed class StockProfitMap : ClassMap<StockProfit>
    {
        public StockProfitMap()
        {
            Map(m => m.FullName);
            Map(m => m.FIGI);
            Map(m => m.Ticker);
            Map(m => m.InvestedAmount);
            Map(m => m.CountBuyStockPossible);
            Map(m => m.PriceAvg);
            Map(m => m.QuantityPaymentsAvg);
            Map(m => m.PayoutAvg);
            Map(m => m.PayoutsYieldAvg);
            Map(m => m.PossibleProfitSpeculation);
        }
    }
}
