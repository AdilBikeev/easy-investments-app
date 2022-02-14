using System.Net;

using CentralBankDailyInfoService;

using Google.Protobuf.Collections;
using Tinkoff.InvestApi.V1;

namespace Stock.API.SyncDataServices.Grps
{
    public class StockDataClient : IStockDataClient
    {
        /// <summary>
        /// Максимальное число для дробной части номинала.
        /// </summary>
        private const uint MAX_NANO = 1000000000;

        /// <summary>
        /// Максимальный интервал запроса данных по свечам.
        /// </summary>
        private const short MAX_YEARS_INTERVAL_CANDLES = 1;

        /// <summary>
        /// Предположительная сумма инвестирования в рублях.
        /// </summary>
        //private const int InvestedAmountRUBDefault = 20000;

        /// <summary>
        /// ClassCode для получения инф. по интсрументу используя FIGI.
        /// </summary>
        private const string ClassCodeDefault = "ClassCode";

        private readonly StockDataClientOptions _options;
        private readonly IMapper _mapper;
        private readonly IValuteDataService _valuteDataService;
        private readonly ILogger<StockDataClient> _logger;
        private readonly string _tinkoffInvestPublicApiURL;
        private readonly string _token;

        /// <remarks />
        public StockDataClient(IOptions<StockDataClientOptions> options, 
                               IMapper mapper,
                               IValuteDataService valuteDataService,
                               ILogger<StockDataClient> logger)
        {
            _options = options.Value;
            _mapper = mapper;
            _valuteDataService = valuteDataService;
            _logger = logger;
            _tinkoffInvestPublicApiURL = _options.TinkoffInvestPublicApiURL;
            _token = _options.AuthToken;
        }

        ///<inheritdoc/>
        public async Task<StockProfitReadDTO> GetProfitByFigi(string figiId, double investedAmount, string currencyFrom)
        {
            var todayDate = DateTime.UtcNow;
            var yearAgoDate = DateTime.UtcNow.AddYears(-MAX_YEARS_INTERVAL_CANDLES);

            var candles = await GetCandlesByFigi(figiId, yearAgoDate, todayDate);
            var currQuotation = candles[candles.Count - 1].Close;

            // Расчитываем средний максимум цены котировки за год
            var avgHighPriceYear = candles.Sum(c => 
                GetMoneyValue(_mapper.Map<MoneyValueDTO>(c.High))
            ) / candles.Count;
            
            var instrument = _mapper.Map<InstrumentDTO>(await GetInstrumentByFigi(figiId));

            // Переводим вложения в валюту котировки
            investedAmount = await _valuteDataService.ConvertValute(
                                amount: investedAmount,
                                currencyFrom: currencyFrom,
                                currencyTo: instrument.Currency);
            var currPrice = GetMoneyValue(_mapper.Map<MoneyValueDTO>(currQuotation));

            //TODO: Сделать, чтобы метод не только дивиденды считал (они только у акций), но и выплаты по облигациям
            var dividends = await GetDividensByFigi(figiId, yearAgoDate, todayDate);

            var countStocs = (int)(investedAmount / currPrice); // кол-во котировок, которое можно купить за потенциалньо вложенные деньги
            var avgProfitYear = countStocs * dividends.AvgPayoutAmount * dividends.QuantityPayments; // средняя прибыль в год
            var avgProfitMounth = avgProfitYear / 12; // средняя прибыль в месяц

            return new StockProfitReadDTO
            {
                AvgProfitMounth = Decimal.Round((decimal)avgProfitMounth, 4),
                AvgProfitYear = Decimal.Round((decimal)avgProfitYear, 4),
                AvgPayoutsYield = Decimal.Round(
                    (decimal)CalcPayoutsYieldYear(currPrice, dividends.AvgPayoutAmount, dividends.QuantityPayments) * 100, 
                    4),
                QuantityPayments = dividends.QuantityPayments,
                PossibleProfitSpeculation = Decimal.Round((decimal)(avgHighPriceYear - currPrice), 4) * countStocs,
                Instrument = instrument
            };
        }

        ///<inheritdoc/>
        public async Task<RepeatedField<HistoricCandle>> GetCandlesByFigi(string figiId, DateTime from, DateTime to)
        {
            var channel = GrpcChannel.ForAddress(_tinkoffInvestPublicApiURL);
            var maketDataService = new MarketDataService.MarketDataServiceClient(channel);
            var headers = new Metadata();
            headers.Add("Authorization", _token);

            var resp = await maketDataService.GetCandlesAsync(new GetCandlesRequest
            {
                Figi = figiId,
                From = Timestamp.FromDateTime(from),
                To = Timestamp.FromDateTime(to),
                Interval = CandleInterval.Day
            }, new CallOptions(headers));

            if (resp is null || resp.Candles.Count == 0)
                throw new ApiException($"{this}.{nameof(GetCandlesByFigi)} error request with {nameof(figiId)}={figiId}", (int)HttpStatusCode.NotFound);

            return resp.Candles;
        }

        /// <summary>
        /// Возвращает информацию по инструменту по FIGI.
        /// </summary>
        /// <param name="figiId">FIJI идентификатор котировки.</param>
        private async Task<Instrument> GetInstrumentByFigi(string figiId)
        {
            var channel = GrpcChannel.ForAddress(_tinkoffInvestPublicApiURL);
            var instrumentsService = new InstrumentsService.InstrumentsServiceClient(channel);
            var headers = new Metadata();
            headers.Add("Authorization", _token);
            var resp = await instrumentsService.GetInstrumentByAsync(new InstrumentRequest
            {
                Id = figiId,
                IdType = InstrumentIdType.Figi,
                ClassCode = ClassCodeDefault
            }, new CallOptions(headers));

            if (resp is null)
                throw new ApiException($"{this}.{nameof(GetInstrumentByFigi)} error request with {nameof(figiId)}={figiId}", (int)HttpStatusCode.NotFound);

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
            var channel = GrpcChannel.ForAddress(_tinkoffInvestPublicApiURL);
            var instrumentsService = new InstrumentsService.InstrumentsServiceClient(channel);
            var headers = new Metadata();
            headers.Add("Authorization", _token);
            var resp = await instrumentsService.GetDividendsAsync(new GetDividendsRequest
            {
                Figi = figiId,
                From = Timestamp.FromDateTime(startDt),
                To = Timestamp.FromDateTime(endDt),
            }, new CallOptions(headers));

            if (resp is null)
                throw new ApiException($"{this}.{nameof(GetDividensByFigi)} error request with {nameof(figiId)}={figiId}", (int)HttpStatusCode.NotFound);

            var dividends = resp.Dividends.Sum(dividend => GetMoneyValue(_mapper.Map<MoneyValueDTO>(dividend.DividendNet)));
            var quantityPayments = resp.Dividends.Count;

            return new PaymentsInfoDTO
            {
                AmountPayments = dividends,
                QuantityPayments = quantityPayments,
                AvgPayoutAmount = quantityPayments != 0 ? dividends / quantityPayments : 0
            };
        }

        /// <summary>
        /// Возвращает цену инструмента по объекту MoneyValueDTO.
        /// </summary>ну в
        private static double GetMoneyValue(MoneyValueDTO moneyValue)
        {
            return moneyValue.Units +
                                                                          moneyValue.Nano / (double)MAX_NANO;
        }

        /// <summary>
        /// Расчитывает % годовую доходность при вложении stockPrice * stockCount.
        /// </summary>
        /// <param name="stockPrice">Цена 1 котировки.</param>
        /// <param name="amountOnePayout">Размер 1-ой выплаты.</param>
        /// <param name="quantityPayments">Кол-во выплат.</param>
        private static double CalcPayoutsYieldYear(
            double stockPrice,
            double amountOnePayout,
            int quantityPayments) => stockPrice != 0 ? (amountOnePayout * quantityPayments) / (stockPrice) : 0;

    }
}
