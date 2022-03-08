using Quotation.Domain.AggregatesModel.QuotationProfitAggregate;
using Quotation.Domain.SeedWork;

namespace Quotation.Domain.AggregatesModel.QuotationAggregate
{
    public class Quotation : Entity<int>, IAggregateRoot
    {
        /// <summary>
        /// Наименование котировки.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// FIGI котировки.
        /// </summary>
        [Required]
        public string FIGI { get; set; }

        /// <summary>
        /// Краткое название в биржевой информации котируемых инструментов (акций, облигаций, индексов).
        /// </summary>
        [Required]
        public string? Ticker { get; set; }

        public QuotationProfit QuotationProfit { get; set; }
    }
}

/// Автосвойства публичные, т.к. используемый данную сущность CsvHelper поддерживает только публичные сеттеры.
