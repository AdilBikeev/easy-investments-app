using Stock.API.Models;
using Stock.API.SyncDataServices.Grps;

namespace Stock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpGet]
        public async Task<ActionResult<StockProfitReadDTO>> GetProfitByFigi([FromQuery] StockProfitRequest stockProfitRequest)
        {
            return Ok(await _stockDataClient.GetProfitByFigi(
                stockProfitRequest.FigiId,
                stockProfitRequest.InvestedAmount,
                stockProfitRequest.CurrencyFrom
                ));
        }
    }
}
