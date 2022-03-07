using System.ComponentModel.DataAnnotations;

using Stock.Domain.SeedWork;

namespace Stock.Domain.AggregatesModel.StockAggregate
{
    /// <summary>
    /// Таблица с информацией прибольности котировок.
    /// </summary>
    public class StockProfit : Entity<int>, IAggregateRoot
    {
        /// <summary>
        /// Полное наименование котировки.
        /// </summary>
        [Required]
        public string FullName { get; private set; }

        /// <summary>
        /// FIGI котировки.
        /// </summary>
        [Required]
        public string FIGI { get; private set; } = default!;

        /// <summary>
        /// Краткое название в биржевой информации котируемых инструментов (акций, облигаций, индексов).
        /// </summary>
        [Required]
        public string? Ticker { get; private set; }

        /// <summary>
        /// Сумма вложений.
        /// </summary>
        [Required]
        public decimal InvestedAmount { get; private set; }

        /// <summary>
        /// Кол-во котировок, которое можно приобрести при данных вложениях.
        /// </summary>
        [Required]
        public int CountBuyStockPossible { get; private set; }

        /// <summary>
        /// Средняя стоимость 1 котировки.
        /// </summary>
        [Required]
        public decimal PriceAvg { get; private set; }

        /// <summary>
        /// Среднее кол-во выплат в год.
        /// </summary>
        [Required]
        public decimal QuantityPaymentsAvg { get; private set; }

        /// <summary>
        /// Средний размер выплаченных дивидендов в год.
        /// </summary>
        [Required]
        public decimal PayoutAvg { get; private set; }

        /// <summary>
        /// Средняя доходность с выплат в % годовых.
        /// </summary>
        [Required]
        public decimal PayoutsYieldAvg { get; init; }

        /// <summary>
        /// Возможная прибыль со спекуляций.
        /// </summary>
        /// <remarks>Расчитывается как: {средний максимум цены котировки за год} - {текущая цена котировки}</remarks>
        [Required]
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

        public StockProfit(in StockProfit stockProfit)
        {
            FullName = stockProfit.FullName;
            FIGI =stockProfit.FIGI;
            Ticker = stockProfit.Ticker;
            InvestedAmount =stockProfit.InvestedAmount;
            PriceAvg =stockProfit.PriceAvg;
            QuantityPaymentsAvg = stockProfit.QuantityPaymentsAvg;
            PayoutAvg = stockProfit.PayoutAvg;
            PayoutsYieldAvg = stockProfit.PayoutsYieldAvg;
            PossibleProfitSpeculation = stockProfit.PossibleProfitSpeculation;
        }
    }
}
