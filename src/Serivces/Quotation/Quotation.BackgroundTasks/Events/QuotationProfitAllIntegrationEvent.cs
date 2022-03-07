using Quotation.BuildingBlocks.EventBus.Events;

namespace Quotation.BackgroundTasks.Events
{
    /// <summary>
    /// Событие для получения Id строки таблицы Quotation.QuotationProfit.
    /// </summary>
    public record QuotationProfitIntegrationEvent : IntegrationEvent
    {
        public int QuotationProfitId { get; }

        public QuotationProfitIntegrationEvent(int QuotationProfitId) =>
            QuotationProfitId = QuotationProfitId;
    }
}
