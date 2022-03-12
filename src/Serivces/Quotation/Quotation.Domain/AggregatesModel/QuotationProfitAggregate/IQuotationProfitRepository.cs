using Quotation.Domain.SeedWork;

namespace Quotation.Domain.AggregatesModel.QuotationProfitAggregate
{
    /// <summary>
    /// Интерфейс для хранилища прибыльности котировок.
    /// </summary>
    public interface IQuotationProfitRepository : IRepository<QuotationProfit>
    {
        /// <summary>
        /// Добавляет данные по объекту <see cref="QuotationProfit"/> в хранилище.
        /// </summary>
        /// <param name="QuotationProfit">Информация о прибыльности котировки.</param>
        Task<QuotationProfit> Add(QuotationProfit QuotationProfit);

        /// <summary>
        /// Обновляет по объекту <see cref="QuotationProfit"/> в хранилище.
        /// </summary>
        /// <param name="QuotationProfit">Информация о прибыльности котировки.</param>
        QuotationProfit Update(QuotationProfit QuotationProfit);

        /// <summary>
        /// Возвращает <see cref="QuotationProfit"/> по quotationId.
        /// </summary>
        /// <param name="quotationId">Уникальный идентификатор котировки.</param>
        Task<QuotationProfit?> FindByQuotationId(int quotationId);
    }
}
