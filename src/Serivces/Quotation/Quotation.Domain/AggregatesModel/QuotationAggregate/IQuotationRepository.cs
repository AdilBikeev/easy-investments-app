using Quotation.Domain.SeedWork;

namespace Quotation.Domain.AggregatesModel.QuotationAggregate
{
    /// <summary>
    /// Интерфейс для хранилища информации о котировках.
    /// </summary>
    public interface IQuotationRepository : IRepository<Quotation>
    {
        /// <summary>
        /// Добавляет или обновляет <see cref="Quotation"/>
        /// </summary>
        /// <param name="quotation">Котировка.</param>
        Task<QuotationAggregate.Quotation> AddOrUpdateAsync(QuotationAggregate.Quotation quotation);

        /// <summary>
        /// Возвращает <see cref="Quotation"/> по FIGI.
        /// </summary>
        /// <param name="figi">Уникальный идентификатор котировки.</param>
        Task<Quotation?> FindByFigiAsync(string figi);
    }
}
