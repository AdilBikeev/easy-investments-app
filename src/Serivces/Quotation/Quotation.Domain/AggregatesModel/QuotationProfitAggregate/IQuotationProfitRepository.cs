using Quotation.Domain.SeedWork;

namespace Quotation.Domain.AggregatesModel.QuotationProfitAggregate
{
    /// <summary>
    /// Интерфейс для хранилища прибыльности котировок.
    /// </summary>
    public interface IQuotationRepository : IRepository<QuotationProfit>
    {
        /// <summary>
        /// Добавляет в хранилище новые данные
        /// </summary>
        /// <param name="QuotationProfit">Информация о прибыльности котировки.</param>
        QuotationProfit Add(QuotationProfit QuotationProfit);

        /// <summary>
        /// Обновляет данные о приболности котировки в хранилище.
        /// </summary>
        /// <param name="QuotationProfit">Информация о прибыльности котировки.</param>
        QuotationProfit Update(QuotationProfit QuotationProfit);
        
        /// <summary>
        /// Возвращает <see cref="QuotationProfit"/> по FIGI.
        /// </summary>
        /// <param name="figi">Уникальный идентификатор котировки.</param>
        Task<QuotationProfit> FindAsync(string figi);
    }
}
