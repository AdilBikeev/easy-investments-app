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
        Task<QuotationProfit> AddQuotationProfitAsync(QuotationProfit QuotationProfit);

        /// <summary>
        /// Обновляет по объекту <see cref="QuotationProfit"/> в хранилище.
        /// </summary>
        /// <param name="QuotationProfit">Информация о прибыльности котировки.</param>
        QuotationProfit UpdateQuotationProfit(QuotationProfit QuotationProfit);

        /// <summary>
        /// Возвращает <see cref="QuotationProfit"/> по FIGI.
        /// </summary>
        /// <param name="figi">Уникальный идентификатор котировки.</param>
        Task<QuotationProfit> FindAsync(string figi);
    }
}
