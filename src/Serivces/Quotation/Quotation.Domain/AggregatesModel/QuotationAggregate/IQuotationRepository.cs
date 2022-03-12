using Quotation.Domain.SeedWork;

namespace Quotation.Domain.AggregatesModel.QuotationAggregate
{
    /// <summary>
    /// Интерфейс для хранилища информации о котировках.
    /// </summary>
    public interface IQuotationRepository : IRepository<Quotation>
    {
        /// <summary>
        /// Добавляет в хранилище новую котировку
        /// </summary>
        /// <param name="Quotation">Котировка.</param>
        Task<Quotation> Add(Quotation Quotation);

        /// <summary>
        /// Обновляет данные котировки.
        /// </summary>
        /// <param name="Quotation">Котировка.</param>
        Quotation Update(Quotation Quotation);
        
        /// <summary>
        /// Возвращает <see cref="Quotation"/> по FIGI.
        /// </summary>
        /// <param name="figi">Уникальный идентификатор котировки.</param>
        Task<Quotation?> FindByFigi(string figi);
    }
}
