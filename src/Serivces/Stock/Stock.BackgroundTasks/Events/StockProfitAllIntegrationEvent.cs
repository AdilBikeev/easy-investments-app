using Stock.BuildingBlocks.EventBus.Events;

namespace Stock.BackgroundTasks.Events
{
    /// <summary>
    /// Событие для получения Id строки таблицы stock.StockProfit.
    /// </summary>
    public record StockProfitIntegrationEvent : IntegrationEvent
    {
        public int StockProfitId { get; }

        public StockProfitIntegrationEvent(int stockProfitId) =>
            StockProfitId = stockProfitId;
    }
}
