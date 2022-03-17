using System.ComponentModel.DataAnnotations.Schema;

using Quotation.Domain.SeedWork;

namespace Quotation.Domain.AggregatesModel.QuotationProfitAggregate
{
    /// <summary>
    /// Таблица с информацией прибольности котировок.
    /// </summary>
    public class QuotationProfit : Entity, IAggregateRoot
    {
        /// <summary>
        /// Сумма вложений.
        /// </summary>
        [Required]
        public decimal InvestedAmount { get; private set; }

        /// <summary>
        /// Кол-во котировок, которое можно приобрести при данных вложениях.
        /// </summary>
        [Required]
        public int CountBuyQuotationPossible { get; private set; }

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
        public decimal PayoutsYieldAvg { get; private set; }

        /// <summary>
        /// Возможная прибыль со спекуляций.
        /// </summary>
        /// <remarks>Расчитывается как: {средний максимум цены котировки за год} - {текущая цена котировки}</remarks>
        [Required]
        public decimal PossibleProfitSpeculation { get; private set; }


        /// <summary>
        /// Идентификатор котировки.
        /// </summary>
        public int QuotationId { get; set; }
        public QuotationAggregate.Quotation Quotation { get; private set; }

        /// <summary>
        /// For EF.
        /// </summary>
        public QuotationProfit()
        {

        }

        /// <summary>
        /// Создает новый объект с идентификатором котировки.
        /// </summary>
        /// <param name="quotationProfit">Объект прибыльности котировки.</param>
        /// <param name="quotationId">Идентификатор котировки.</param>
        public QuotationProfit CopyTo(QuotationProfit quotationProfit, int quotationId)
            => new QuotationProfit(quotationProfit, quotationId);

        private QuotationProfit(QuotationProfit quotationProfit, int quotationId)
        {
            InvestedAmount = quotationProfit.InvestedAmount;
            CountBuyQuotationPossible = quotationProfit.CountBuyQuotationPossible;
            PriceAvg = quotationProfit.PriceAvg;
            QuantityPaymentsAvg = quotationProfit.QuantityPaymentsAvg;
            PayoutAvg = quotationProfit.PayoutAvg;
            PayoutsYieldAvg = quotationProfit.PayoutsYieldAvg;
            PossibleProfitSpeculation = quotationProfit.PossibleProfitSpeculation;

            QuotationId = quotationId;
        }
    }
}
