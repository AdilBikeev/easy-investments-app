namespace Stock.API.Models
{
    /// TODO: Перенести models на уровень ApiGateways/Web.Bff.EasyInvest
    /// Bff - Backend For Frontend
    public record StockProfitRequest
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
        /// Код валюты вложений.
        /// </summary>
        [Required]
        [DefaultValue(CurrencyCode.RUB)]
        [JsonConverter(typeof(StringEnumConverter))]
        public CurrencyCode CurrencyFrom { get; init; }
    }
}
