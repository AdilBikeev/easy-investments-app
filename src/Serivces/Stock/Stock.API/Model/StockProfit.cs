using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Stock.API.Model
{
    public record StockProfit
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
        [MaxLength(4)]
        [DefaultValue("RUB")]
        public string CurrencyFrom { get; init; }

        /// <summary>
        /// Начало запрашиваемого периода в часовом поясе UTC.
        /// </summary>
        [Required]
        [DefaultValue("2017-02-06T21:48:05.826802400Z")]
        public DateTime DateFrom { get; init; }

        /// <summary>
        /// Окончание запрашиваемого периода в часовом поясе UTC.
        /// </summary>
        [Required]
        [DefaultValue("2022-02-06T21:48:05.826802400Z")]
        public DateTime DateTo { get; init; }
    }
}
