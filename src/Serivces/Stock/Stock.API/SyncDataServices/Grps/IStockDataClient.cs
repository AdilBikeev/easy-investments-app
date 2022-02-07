using Stock.API.DTOs;

namespace Stock.API.SyncDataServices.Grps;

public interface IStockDataClient
{
    /// <summary>
    /// Возвращает информацию о прибыльности котировки.
    /// </summary>
    /// <param name="figiId">FIGI идентификаор котирвоки./param>
    /// <param name="investedAmount">Сумма возможных инвестиций в котировку.</param>
    /// <param name="currencyFrom">Код валюты вложений.</param>
    public Task<StockProfitReadDTO> GetProfitByFigi(string figiId, double investedAmount, CurrencyCode currencyFrom);
}