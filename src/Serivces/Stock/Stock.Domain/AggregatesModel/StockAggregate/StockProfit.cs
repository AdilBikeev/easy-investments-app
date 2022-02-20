using Stock.Domain.SeedWork;

namespace Stock.Domain.AggregatesModel.StockAggregate
{
    /// <summary>
    /// Таблица с информацией прибольности котировок.
    /// </summary>
    public class StockProfit : Entity, IAggregateRoot
    {
        /// <summary>
        /// Полное наименование котировки.
        /// </summary>
        public string FullName { get; private set; }

        /// <summary>
        /// FIGI котировки.
        /// </summary>
        public string FIGI { get; private set; } = default!;

        /// <summary>
        /// Краткое название в биржевой информации котируемых инструментов (акций, облигаций, индексов).
        /// </summary>
        public string? Ticker { get; private set; }

        /// <summary>
        /// Сумма вложений.
        /// </summary>
        public decimal InvestedAmount { get; private set; }

        /// <summary>
        /// Кол-во котировок, которое можно приобрести при данных вложениях.
        /// </summary>
        public int CountBuyStockPossible { get; private set; }

        /// <summary>
        /// Средняя стоимость 1 котировки.
        /// </summary>
        public decimal PriceAvg { get; private set; }

        /// <summary>
        /// Среднее кол-во выплат в год.
        /// </summary>
        public decimal QuantityPaymentsAvg { get; private set; }

        /// <summary>
        /// Средний размер выплаченных дивидендов в год.
        /// </summary>
        public decimal PayoutAvg { get; private set; }

        /// <summary>
        /// Средняя доходность с выплат в % годовых.
        /// </summary>
        public decimal PayoutsYieldAvg { get; init; }

        /// <summary>
        /// Возможная прибыль со спекуляций.
        /// </summary>
        /// <remarks>Расчитывается как: {средний максимум цены котировки за год} - {текущая цена котировки}</remarks>
        public decimal PossibleProfitSpeculation { get; init; }

        public StockProfit(
            string fullName,
            string figi,
            string ticker,
            decimal investedAmount,
            decimal priceAvg,
            decimal quantityPaymentsAvg,
            decimal payoutAvg,
            decimal payoutsYieldAvg,
            decimal possibleProfitSpeculation
            )
        {
            FullName = fullName;
            FIGI = figi;
            Ticker = ticker;
            InvestedAmount = investedAmount;
            PriceAvg = priceAvg;
            QuantityPaymentsAvg = quantityPaymentsAvg;
            PayoutAvg = payoutAvg;
            PayoutsYieldAvg = payoutsYieldAvg;
            PossibleProfitSpeculation = possibleProfitSpeculation;
        }
    }
}
