using Quotation.API.Application.Commands;
using Quotation.API.Configuration;
using Quotation.API.DTOs;
using Quotation.API.Infrastracture.Exceptions;
using Quotation.API.SyncDataServices.Soap;


namespace Quotation.API.SyncDataServices.Grps
{
    /// <summary>
    /// Интерфейс для работы с котировками.
    /// </summary>
    public interface IQuotationDataClient
    {
        /// <summary>
        /// Возвращает информацию о прибыльности котировки.
        /// </summary>
        /// <param name="figiId">FIGI идентификаор котирвоки.</param>
        /// <param name="investedAmount">Сумма возможных инвестиций в котировку.</param>
        /// <param name="currencyFrom">Код валюты вложений.</param>
        /// <param name="dateFrom">Начало запрашиваемого периода в часовом поясе UTC.</param>
        /// <param name="dateTo">Окончание запрашиваемого периода в часовом поясе UTC.</param>
        public Task<QuotationProfitReadDTO> GetProfitByFigi(string figiId, decimal investedAmount, string currencyFrom, DateTime dateFrom, DateTime dateTo);
    }

    public class QuotationDataClient : IQuotationDataClient
    {
        /// <summary>
        /// Максимальный интервал запроса данных по свечам.
        /// </summary>
        private const short MAX_YEARS_INTERVAL_CANDLES = 1;

        /// <summary>
        /// Максимальный период просмотра свечей в днях.
        /// </summary>
        private const short MAX_DAYS_INTERVAL_CANDLES = 365;

        /// <summary>
        /// ClassCode для получения инф. по интсрументу используя FIGI.
        /// </summary>
        private const string ClassCodeDefault = "ClassCode";

        /// <summary>
        /// Сервис получения биржевой информации. 
        /// </summary>
        private readonly MarketDataService.MarketDataServiceClient _marketDataService;

        /// <summary>
        /// Сервис предоставления справочной информации о ценных бумагах.
        /// </summary>
        private readonly InstrumentsService.InstrumentsServiceClient _instrumentsService;
        private readonly IMediator _mediator;
        private readonly QuotationDataClientOptions _options;
        private readonly IMapper _mapper;
        private readonly IValuteDataService _valuteDataService;
        private readonly ILogger<QuotationDataClient> _logger;

        /// <remarks />
        public QuotationDataClient(IOptions<QuotationDataClientOptions> options,
                               IMapper mapper,
                               Tinkoff.InvestApi.InvestApiClient investApiClient,
                               IValuteDataService valuteDataService,
                               IMediator mediator,
                               ILogger<QuotationDataClient> logger)
        {
            _options = options.Value;
            _mapper = mapper;
            _valuteDataService = valuteDataService;
            _marketDataService = investApiClient.MarketData;
            _instrumentsService = investApiClient.Instruments;
            _mediator = mediator;
            _logger = logger;
        }

        ///<inheritdoc/>
        public async Task<QuotationProfitReadDTO> GetProfitByFigi(string figiId, decimal investedAmount, string currencyFrom, DateTime dateFrom, DateTime dateTo)
        {
            var candles = await GetCandlesByFigi(figiId, dateFrom, dateTo);
            var currQuotation = candles[candles.Count - 1].Close;

            // Расчитываем средний максимум цены котировки за год
            var avgHighPriceYear = candles.Sum(c =>
                (decimal)_mapper.Map<MoneyValueDTO>(c.High)
            ) / candles.Count; 

            var instrument = _mapper.Map<InstrumentDTO>(await GetInstrumentByFigi(figiId));

            // Переводим вложения в валюту котировки
            investedAmount = await _valuteDataService.ConvertValute(
                                amount: investedAmount,
                                currencyFrom: currencyFrom,
                                currencyTo: instrument.Currency);
            var currPrice = (decimal)_mapper.Map<MoneyValueDTO>(currQuotation);

            //TODO: Сделать, чтобы метод не только дивиденды считал (они только у акций), но и выплаты по облигациям
            var dividends = await GetDividensByFigi(figiId, dateFrom, dateTo);

            var countStocs = (int)(investedAmount / currPrice); // кол-во котировок, которое можно купить за потенциалньо вложенные деньги
            var avgProfitPeriod = countStocs * dividends.AvgPayoutAmount * dividends.QuantityPayments; // средняя прибыль за заданный период
            var avgProfitYear = avgProfitPeriod /
                                ((dateTo - dateFrom).Days / 365); // средняя прибыль за год
            var avgProfitMounth = avgProfitYear / 12; // средняя прибыль в месяц

            var quotationProfitReadDTO = new QuotationProfitReadDTO
            {
                AvgProfitMounth = Decimal.Round(avgProfitMounth, 4),
                AvgProfitYear = Decimal.Round(avgProfitYear, 4),
                AvgPayoutsYield = Decimal.Round(
                    CalcPayoutsYieldYear(dividends.AvgPayoutAmount, dividends.QuantityPayments, currPrice) * 100,
                    4),
                QuantityPayments = dividends.QuantityPayments,
                PossibleProfitSpeculation = Decimal.Round(avgHighPriceYear - currPrice, 4) * countStocs,
                Instrument = instrument
            };

            return quotationProfitReadDTO;
        }

        ///<inheritdoc/>
        public async Task<RepeatedField<HistoricCandle>> GetCandlesByFigi(string figiId, DateTime from, DateTime to)
        {
            var candles = new RepeatedField<HistoricCandle>();

            if ((to - from).Days > MAX_DAYS_INTERVAL_CANDLES)
            {
                var currTo = to;
                var currFrom = currTo.AddYears(-MAX_YEARS_INTERVAL_CANDLES);

                // округляем, чтобы не терялясь погрешность в несколько днях из-за вычитания года на каждой итерации.
                if (currFrom < from)
                    currFrom = from;

                // Если превышаем интервал за счет високосного года
                if ((currTo - currFrom).Days > MAX_DAYS_INTERVAL_CANDLES)
                    currFrom = currFrom.AddDays(1);

                // Пока не пршлись по всему периоду
                while (currFrom >= from && currTo > currFrom)
                {
                    var resp = await _marketDataService.GetCandlesAsync(new GetCandlesRequest
                    {
                        Figi = figiId,
                        From = Timestamp.FromDateTime(currFrom),
                        To = Timestamp.FromDateTime(currTo),
                        Interval = CandleInterval.Day
                    });

                    if (resp is null || resp.Candles.Count == 0)
                        throw new ApiException($"{this}.{nameof(GetCandlesByFigi)} error request with {nameof(figiId)}={figiId}", (int)HttpStatusCode.NotFound);
                    candles.AddRange(resp.Candles);

                    currTo = currFrom;
                    currFrom = currTo.AddYears(-MAX_YEARS_INTERVAL_CANDLES);

                    // округляем, чтобы не терялясь погрешность в несколько днях из-за вычитания года на каждой итерации.
                    if (currFrom < from)
                        currFrom = from;

                    // Если превышаем интервал за счет високосного года
                    if ((currTo - currFrom).Days > MAX_DAYS_INTERVAL_CANDLES)
                        currFrom = currFrom.AddDays(1);
                }
            }
            else
            {
                var resp = await _marketDataService.GetCandlesAsync(new GetCandlesRequest
                {
                    Figi = figiId,
                    From = Timestamp.FromDateTime(from),
                    To = Timestamp.FromDateTime(to),
                    Interval = CandleInterval.Day
                });

                if (resp is null || resp.Candles.Count == 0)
                    throw new ApiException($"{this}.{nameof(GetCandlesByFigi)} error request with {nameof(figiId)}={figiId}", (int)HttpStatusCode.NotFound);
                candles.AddRange(resp.Candles);
            }

            return candles;
        }

        /// <summary>
        /// Возвращает информацию по инструменту по FIGI.
        /// </summary>
        /// <param name="figiId">FIJI идентификатор котировки.</param>
        private async Task<Instrument> GetInstrumentByFigi(string figiId)
        {
            var resp = await _instrumentsService.GetInstrumentByAsync(new InstrumentRequest
            {
                Id = figiId,
                IdType = InstrumentIdType.Figi,
                ClassCode = ClassCodeDefault
            });

            if (resp is null)
                throw new ApiException($"{this}.{nameof(GetInstrumentByFigi)} error request with {nameof(figiId)}={figiId}", (int)HttpStatusCode.NotFound);

            var instrument = resp.Instrument;
            _mediator.Send(new CreateOrUpdateQuotationCommand(
                instrument.Figi, 
                instrument.Name, 
                instrument.Ticker
            ));

            return resp.Instrument;
        }

        /// <summary>
        /// Возвращает информацию по выплаченным дивидендам за заданный период.
        /// </summary>
        /// <param name="figiId">FIGI идентификатор котировки.</param>
        /// <param name="startDt">Начало рассматриваемого периода выплачевания дивидендов.</param>
        /// <param name="endDt">Конец рассматриваемого периода выплачевания дивидендов.</param>
        private async Task<PaymentsInfoDTO> GetDividensByFigi(string figiId, DateTime startDt, DateTime endDt)
        {
            var resp = await _instrumentsService.GetDividendsAsync(new GetDividendsRequest
            {
                Figi = figiId,
                From = Timestamp.FromDateTime(startDt),
                To = Timestamp.FromDateTime(endDt),
            });

            if (resp is null)
                throw new ApiException($"{this}.{nameof(GetDividensByFigi)} error request with {nameof(figiId)}={figiId}", (int)HttpStatusCode.NotFound);

            var dividends = resp.Dividends.Sum(dividend => _mapper.Map<MoneyValueDTO>(dividend.DividendNet));
            var quantityPayments = resp.Dividends.Count;

            return new PaymentsInfoDTO
            {
                AmountPayments = dividends,
                QuantityPayments = quantityPayments,
                AvgPayoutAmount = quantityPayments != 0 ? dividends / quantityPayments : 0
            };
        }

        /// <summary>
        /// Расчитывает % годовую доходность при вложении QuotationPrice * QuotationCount.
        /// </summary>
        /// <param name="amountOnePayout">Размер 1-ой выплаты.</param>
        /// <param name="quantityPayments">Кол-во выплат.</param>
        /// <param name="QuotationPrice">Цена 1 котировки.</param>
        private static decimal CalcPayoutsYieldYear(
            decimal amountOnePayout,
            int quantityPayments,
            decimal QuotationPrice) => QuotationPrice != 0 ? (amountOnePayout * quantityPayments) / (QuotationPrice) : 0;
    }
}
