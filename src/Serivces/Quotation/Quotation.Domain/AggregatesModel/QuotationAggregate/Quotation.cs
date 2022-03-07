﻿using Quotation.Domain.SeedWork;

namespace Quotation.Domain.AggregatesModel.QuotationAggregate
{
    public class Quotation : Entity<int>, IAggregateRoot
    {
        /// <summary>
        /// Наименование котировки.
        /// </summary>
        [Required]
        public string Name { get; private set; }

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
    }
}
