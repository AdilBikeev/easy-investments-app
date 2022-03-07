using Quotation.API.DTOs;
using Quotation.API.Model;
using Quotation.API.SyncDataServices.Grps;

namespace Quotation.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с котировками.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class QuotationController : ControllerBase
    {
        private readonly IQuotationDataClient _QuotationDataClient;

        public QuotationController(
            IQuotationDataClient QuotationDataClient)
        {
            _QuotationDataClient = QuotationDataClient;
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
        public async Task<ActionResult<QuotationProfitReadDTO>> GetProfitByFigi([FromQuery] QuotationProfit QuotationProfit)
        {
            return Ok(await _QuotationDataClient.GetProfitByFigi(
                QuotationProfit.FigiId,
                QuotationProfit.InvestedAmount,
                QuotationProfit.CurrencyFrom,
                QuotationProfit.DateFrom,
                QuotationProfit.DateTo
                ));
        }
    }
}
