using Microsoft.AspNetCore.Mvc;

using Stock.API.Model;
using Stock.API.SyncDataServices.Grps;

namespace Stock.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с котировками.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockDataClient _stockDataClient;

        public StockController(
            IStockDataClient stockDataClient)
        {
            _stockDataClient = stockDataClient;
        }

        /// <summary>
        /// Возвращает информацию о прибыльности котировки по FIGI идентификатору.
        /// </summary>
        /// <param name="figiId">FIGI идентификаор котирвоки./param>
        /// <param name="investedAmount">Сумма возможных инвестиций в котировку.</param>
        /// <param name="currencyFrom">Код валюты вложений.</param>
        /// <param name="currencyTo">Код валюты для отображения.</param>
        [HttpGet("profit",Name = nameof(GetProfitByFigi))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StockProfitReadDTO>> GetProfitByFigi([FromQuery] StockProfit stockProfit)
        {
            return Ok(await _stockDataClient.GetProfitByFigi(
                stockProfit.FigiId,
                stockProfit.InvestedAmount,
                stockProfit.CurrencyFrom,
                stockProfit.DateFrom,
                stockProfit.DateTo
                ));
        }
    }
}
