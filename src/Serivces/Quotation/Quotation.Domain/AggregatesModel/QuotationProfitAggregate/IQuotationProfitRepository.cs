﻿using Quotation.Domain.SeedWork;

namespace Quotation.Domain.AggregatesModel.QuotationProfitAggregate
{
    /// <summary>
    /// Интерфейс для хранилища прибыльности котировок.
    /// </summary>
    public interface IQuotationProfitRepository : IRepository<QuotationProfit>
    {
        /// Возвращает <see cref="QuotationProfit"/> по quotationId.
        /// </summary>
        /// <param name="quotationId">Уникальный идентификатор котировки.</param>
        Task<QuotationProfit?> FindByQuotationIdAsync(int quotationId);

        /// <summary>
        /// Добавляет или обновляет <see cref="QuotationProfit"/>
        /// </summary>
        /// <param name="quotationProfit">Прибольность котировки.</param>
        /// <param name="quotationProfitId">Идентификатро сущности <see cref="QuotationProfit"/>.</param>
        Task<QuotationProfit> AddOrUpdateAsync(QuotationProfit quotationProfit, int quotationProfitId);
    }
}
