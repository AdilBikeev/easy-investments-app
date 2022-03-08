﻿using System.ComponentModel.DataAnnotations.Schema;

using Quotation.Domain.SeedWork;

namespace Quotation.Domain.AggregatesModel.QuotationProfitAggregate
{
    /// <summary>
    /// Таблица с информацией прибольности котировок.
    /// </summary>
    public class QuotationProfit : Entity<int>, IAggregateRoot
    {
        private string _figi;

        /// <summary>
        /// Полное наименование котировки.
        /// </summary>
        [Required]
        public string FullName { get; private set; }

        /// <summary>
        /// FIGI котировки.
        /// </summary>
        [Required]
        public string FIGI { get; private set; }

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
    }
}
