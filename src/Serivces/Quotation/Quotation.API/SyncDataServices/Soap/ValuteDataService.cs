namespace Quotation.API.SyncDataServices.Soap
{
    /// <summary>
    /// Интерфейс сервиса для работы с валютами.
    /// </summary>
    public interface IValuteDataService
    {
        /// <summary>
        /// Конвертирует сумму amount из currencyFrom в currencyTo.
        /// </summary>
        /// <param name="amount">Сумма для конвертации.</param>
        /// <param name="currencyFrom">Код валюты исходной суммы amount.</param>
        /// <param name="currencyTo">Код валюты, в которую нужно конвертировать.</param>
        Task<decimal> ConvertValute(decimal amount, string currencyFrom, string currencyTo);
    }

    /// <summary>
    /// Сервис для работы с валютами.
    /// </summary>
    public class ValuteDataService : IValuteDataService
    {
        //TODO: возможны проблемы с Hot-Reaload. При его использовании запросы к сервису 
        // падают с исключением.
        // Временное решение - перезапустить приложение.
        private readonly ICentralBankService _centralBankService;

        public ValuteDataService(ICentralBankService centralBankService)
        {
            _centralBankService = centralBankService;
        }

        /// <inheritdoc/>
        public async Task<decimal> ConvertValute(decimal amount, string currencyFrom, string currencyTo)
        {
            // Если конвертация бессмыслена
            if (currencyFrom.Equals(currencyTo))
                return amount;

            currencyFrom = currencyFrom.ToUpperInvariant();
            currencyTo = currencyTo.ToUpperInvariant();

            decimal amountRub = amount, // сумма в рублях
                    amountCurrencyTo; // сумма в новой валюте

            // Список курсов валют на сегодня
            var cursOnDate = await _centralBankService.GetCursOnDateXMLAsync(DateTime.Now);

            // Если текущая валюта НЕ рубли - переводим в рубли
            if (!currencyFrom.Equals("RUB"))
                amountRub = ConvertToRUB(
                                    amount,
                                    cursTo: cursOnDate.First(v => v.VchCode.Equals(currencyFrom))
                                );

            // Если валюта в которую нужно перевести валюта - рубли
            if (currencyTo.Equals("RUB"))
                amountCurrencyTo = amountRub;
            else // инчае
                amountCurrencyTo = ConvertFromRubToCurrency(
                                    amountRub,
                                    cursTo: cursOnDate.First(v => v.VchCode.Equals(currencyTo))
                                   );

            return amountCurrencyTo;

            decimal ConvertToRUB(decimal amount, ValuteCursOnDate cursTo) => amount *
                                                                             (cursTo.Vcurs / cursTo.Vnom);

            decimal ConvertFromRubToCurrency(decimal amountRub, ValuteCursOnDate cursTo) => amountRub / (cursTo.Vcurs / cursTo.Vnom);
        }
    }
}
