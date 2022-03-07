using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using EasyInvestments.Quotation.API.V1;

namespace Web.EasyInvestments.HttpAggregator.Models
{
    public record QuotationProfitRequest
    {
        /// <summary>
        /// FIGI идентификатор котировки.
        /// </summary>
        [Required]
        [MaxLength(12)]
        [DefaultValue("BBG000BWQFY7")] // Wells Fargo
        public string FigiId { get; init; }

        /// <summary>
        /// Сумма возможных инвестиций в котировку.
        /// </summary>
        [Required]
        [DefaultValue(20000)]
        public long InvestedAmount { get; init; }

        /// <summary>
        /// Валюта вложений.
        /// </summary>
        [Required]
        public CurrencyCode CurrencyFrom { get; init; }
    }
}
