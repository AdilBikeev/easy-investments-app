using Stock.Domain.SeedWork;

namespace Stock.Domain.AggregatesModel.StockAggregate
{
    /// <summary>
    /// Интерфейс для хранилища прибыльности котировок.
    /// </summary>
    public interface IStockProfitRepository : IRepository<StockProfit>
    {
        /// <summary>
        /// Добавляет в хранилище новые данные
        /// </summary>
        /// <param name="stockProfit">Информация о прибыльности котировки.</param>
        StockProfit Add(StockProfit stockProfit);
    }
}
