namespace Stock.API.Configuration
{
    public record StockDataClientOptions
    {
        /// <summary>
        /// URL к API Тинькофф.
        /// </summary>
        public string TinkoffInvestPublicApiURL { get; init; }

        /// <summary>
        /// Токен авторизации Тинькофф.
        /// </summary>
        public string AuthToken { get; init; } //TODO: В будущем для снятия ограничения в лимитных запросах - сделать авторизацию по токену пользователя.
    }
}
