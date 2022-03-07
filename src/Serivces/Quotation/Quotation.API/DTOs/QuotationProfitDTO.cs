namespace Quotation.API.DTOs
{
    /// <summary>
    /// Объект, описываюются прибыль котировки.
    /// </summary>
    public record QuotationProfitDTO
    {
        /// <summary>
        /// Средняя прибыль за год.
        /// </summary>
        public double AvgProfitYear { get; init; }

        /// <summary>
        /// Средняя прибыль за месяц.
        /// </summary>
        public double AvgProfitMounth { get; init; }

        /// <summary>
        /// Средняя дивидендная доходность в % годовых.
        /// </summary>
        public decimal AvgDividendYieldYear { get; init; }
    }
}
