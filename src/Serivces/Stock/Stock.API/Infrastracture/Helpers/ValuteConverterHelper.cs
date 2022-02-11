using Stock.API.Infrastracture.Enums;

namespace Stock.API.Infrastracture.Helpers
{
    //TODO: Заменить на онлайн сервис конвертирование валюты.
    public static class ValuteConverterHelper
    {
        /// <summary>
        /// Предположительная стоимость доллора к рублю.
        /// </summary>
        private const int CourseDollarRubDefault = 70;

        /// <summary>
        /// Словарь сопоставляющий код валюы с enums CurrencyCode.
        /// </summary>
        public static readonly Dictionary<string, CurrencyCode> CurrencyCodeDict = new Dictionary<string, CurrencyCode>
        {
            ["usd"] = CurrencyCode.USD, 
            ["rub"] = CurrencyCode.RUB, 
        };

        /// <summary>
        /// Конвертирует сумму amount из currencyFrom в currencyTo.
        /// </summary>
        /// <param name="amount">Сумма для конвертации.</param>
        /// <param name="currencyFrom">Код валюты исходной суммы amount.</param>
        /// <param name="currencyTo">Код валюты, в которую нужно конвертировать amount.</param>
        /// <exception cref="NotImplementedException"></exception>
        public static double ConvertValute(double amount, CurrencyCode currencyFrom, CurrencyCode currencyTo)
        {
            switch (currencyFrom)
            {
                case CurrencyCode.USD:
                    switch (currencyTo)
                    {
                        case CurrencyCode.USD:
                            return amount;
                        case CurrencyCode.RUB:
                            return amount * CourseDollarRubDefault;
                        default:
                            throw new NotImplementedException($"Не реализона логика конвертации валюты из {currencyFrom} в {currencyTo}");
                    }
                case CurrencyCode.RUB:
                    switch (currencyTo)
                    {
                        case CurrencyCode.USD:
                            return amount / CourseDollarRubDefault;
                        case CurrencyCode.RUB:
                            return amount;
                        default:
                            throw new NotImplementedException($"Не реализона логика конвертации валюты из {currencyFrom} в {currencyTo}");
                    }
                default:
                    throw new NotImplementedException($"Не реализона логика конвертации валюты из {currencyFrom} в {currencyTo}");
            }
        }
    }
}
