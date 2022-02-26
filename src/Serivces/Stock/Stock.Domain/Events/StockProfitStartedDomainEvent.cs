namespace Stock.Domain.Events
{
    /// <summary>
    /// Событие используется во время создание/обновления данных в таблие StockProfit.
    /// </summary>
    public class StockProfitStartedDomainEvent : INotification
    {
        /// <summary>
        /// Полное наименование котировки.
        /// </summary>
        public string FullName { get; }

        /// <summary>
        /// FIGI котировки.
        /// </summary>
        public string FIGI { get; } = default!;

        /// <summary>
        /// Краткое название в биржевой информации котируемых инструментов (акций, облигаций, индексов).
        /// </summary>
        public string? Ticker { get; }

        /// <summary>
        /// Сумма вложений.
        /// </summary>
        public decimal InvestedAmount { get; }

        /// <summary>
        /// Кол-во котировок, которое можно приобрести при данных вложениях.
        /// </summary>
        public int CountBuyStockPossible { get; }

        /// <summary>
        /// Средняя стоимость 1 котировки.
        /// </summary>
        public decimal PriceAvg { get; }

        /// <summary>
        /// Среднее кол-во выплат в год.
        /// </summary>
        public decimal QuantityPaymentsAvg { get; }

        /// <summary>
        /// Средний размер выплаченных дивидендов в год.
        /// </summary>
        public decimal PayoutAvg { get; }

        /// <summary>
        /// Средняя доходность с выплат в % годовых.
        /// </summary>
        public decimal PayoutsYieldAvg { get; }

        /// <summary>
        /// Возможная прибыль со спекуляций.
        /// </summary>
        /// <remarks>Расчитывается как: {средний максимум цены котировки за год} - {текущая цена котировки}</remarks>
        public decimal PossibleProfitSpeculation { get; }

        public StockProfitStartedDomainEvent(
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
