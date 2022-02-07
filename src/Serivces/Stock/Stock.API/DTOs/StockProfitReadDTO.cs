namespace Stock.API.DTOs
{
    /// <summary>
    /// Объект, описываюются прибыль котировки.
    /// Применяется для GET запросов.
    /// </summary>
    public record StockProfitReadDTO
    {
        /// <summary>
        /// Средняя прибыль за год.
        /// </summary>
        public decimal AvgProfitYear { get; init; }

        /// <summary>
        /// Средняя прибыль за месяц.
        /// </summary>
        public decimal AvgProfitMounth { get; init; }

        /// <summary>
        /// Средняя доходность в % годовых.
        /// </summary>
        public decimal AvgPayoutsYield { get; init; }

        /// <summary>
        /// Кол-во выплат за год.
        /// </summary>
        public int QuantityPayments { get; init; }

        /// <summary>
        /// Возможная прибыль со спекуляций.
        /// </summary>
        /// <remarks>Расчитывается как: {средний максимум цены котировки за год} - {текущая цена котировки}</remarks>
        public decimal PossibleProfitSpeculation { get; init; }

        /// <summary>
        /// Информация по инструменту.
        /// </summary>
        public InstrumentDTO Instrument { get; init; }
    }
}
