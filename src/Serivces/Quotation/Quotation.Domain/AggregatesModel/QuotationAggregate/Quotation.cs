using Quotation.Domain.AggregatesModel.QuotationProfitAggregate;
using Quotation.Domain.SeedWork;

namespace Quotation.Domain.AggregatesModel.QuotationAggregate
{
    public class Quotation : Entity, IAggregateRoot
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
        
        /// <summary>
        /// For EF.
        /// </summary>
        public Quotation()
        {

        }

        public Quotation(string name, string fIGI, string? ticker, QuotationProfit quotationProfit)
        {
            Name = name;
            FIGI = fIGI;
            Ticker = ticker;
            QuotationProfit = quotationProfit;
        }

        /// <summary>
        /// Создает новый объект с Id текущего объекта.
        /// </summary>
        /// <param name="quotation">Объект котировки.</param>
        /// <param name="id">Идентификатор котировки.</param>
        public Quotation CopyTo(Quotation quotation)
            => new Quotation(quotation, this.Id);

        private Quotation(Quotation quotation,int id)
        {
            Id = id;
            Name = quotation.Name;
            FIGI = quotation.FIGI;
            Ticker = quotation.Ticker;
        }
    }
}

/// Автосвойства публичные, т.к. используемый данную сущность CsvHelper поддерживает только публичные сеттеры.
