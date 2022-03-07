using EasyInvestments.Quotation.API.V1;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Web.EasyInvestments.HttpAggregator.Models;

namespace Web.EasyInvestments.HttpAggregator.Controller
{
    /// <summary>
    /// Контроллер для работы с котировками.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class QuotationController : ControllerBase
    {
        private IQuotationClient _QuotationClient;

        public QuotationController(
            IQuotationClient QuotationClient)
        {
            _QuotationClient = QuotationClient;
        }

        /// <summary>
        /// Возвращает информацию о прибыльности котировки по FIGI идентификатору.
        /// </summary>
        /// <param name="figiId">FIGI идентификаор котирвоки./param>
        /// <param name="investedAmount">Сумма возможных инвестиций в котировку.</param>
        /// <param name="currencyFrom">Код валюты вложений.</param>
        /// <param name="currencyTo">Код валюты для отображения.</param>
        [HttpGet("profit", Name = nameof(GetProfitByFigi))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<QuotationProfitReadDTO>> GetProfitByFigi([FromQuery] QuotationProfitRequest QuotationProfit)
        {
            return Ok(await _QuotationClient.GetProfitByFigiAsync(
                QuotationProfit.FigiId,
                QuotationProfit.InvestedAmount,
                QuotationProfit.CurrencyFrom
                ));
        }
    }
}
