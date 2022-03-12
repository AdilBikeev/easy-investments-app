namespace Quotation.API.Application.Commands
{
    /// <summary>
    /// Бизнес команда для создания или обновления инфомации по прибольности котировки
    /// в БД.
    /// </summary>
    public class CreateOrUpdateQuotationProfitCommand
        : IRequest<bool>
    {
        /// <summary>
        /// Сумма вложений.
        /// </summary>
        public decimal InvestedAmount { get; private set; }

        /// <summary>
        /// Кол-во котировок, которое можно приобрести при данных вложениях.
        /// </summary>
        public int CountBuyQuotationPossible { get; private set; }

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
        public decimal PayoutsYieldAvg { get; private set; }

        /// <summary>
        /// Возможная прибыль со спекуляций.
        /// </summary>
        /// <remarks>Расчитывается как: {средний максимум цены котировки за год} - {текущая цена котировки}</remarks>
        public decimal PossibleProfitSpeculation { get; private set; }

        /// <summary>
        /// FIGI идентификатор котировки.
        /// </summary>
        public string FIGI { get; private set; }

        /// <summary>
        /// Наименование котировки.
        /// </summary>
        public string Name { get; private set; }

        public CreateOrUpdateQuotationProfitCommand(
            decimal investedAmount,
            int countBuyQuotationPossible,
            decimal priceAvg,
            decimal quantityPaymentsAvg,
            decimal payoutAvg,
            decimal payoutsYieldAvg,
            decimal possibleProfitSpeculation,
            string fIGI, 
            string name)
        {
            InvestedAmount = investedAmount;
            CountBuyQuotationPossible = countBuyQuotationPossible;
            PriceAvg = priceAvg;
            QuantityPaymentsAvg = quantityPaymentsAvg;
            PayoutAvg = payoutAvg;
            PayoutsYieldAvg = payoutsYieldAvg;
            PossibleProfitSpeculation = possibleProfitSpeculation;
            FIGI = fIGI;
            Name = name;
        }
    }
}
