using System.Data;

using Dapper;

using Npgsql;

using Stock.BackgroundTasks.Configurations;
using Stock.BackgroundTasks.Events;
using Stock.BuildingBlocks.EventBus.Abstractions;

namespace Stock.BackgroundTasks.Services
{
    public class StockPorfitManagerService : BackgroundService
    {
        private readonly BackgroundTaskOptions _options;
        private readonly IEventBus _eventBus;
        private readonly ILogger<StockPorfitManagerService> _logger;

        public StockPorfitManagerService(
            IOptions<BackgroundTaskOptions> options, 
            IEventBus eventBus, 
            ILogger<StockPorfitManagerService> logger)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        ///<inheritdoc/>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"{this} is starting.");

            stoppingToken.Register(() => _logger.LogDebug($"#1 {this} background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug($"{this} background task is doing background work.");

                //TODO: Do somthing
                SendAllStockProfitIds();

                await Task.Delay(_options.CheckUpdateTime, stoppingToken);
            }

            _logger.LogDebug($"{this}  background task is stopping.");
        }

        /// <summary>
        /// Помещает в шину данных Id сущностей таблицы stock.StockPorfit.
        /// </summary>
        private void SendAllStockProfitIds()
        {
            _logger.LogDebug($"Start call {nameof(GetAllStockProfitId)}");

            var orderIds = GetAllStockProfitId();

            foreach (var orderId in orderIds)
            {
                var stockProfitEvent = new StockProfitIntegrationEvent(orderId);

                _logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", 
                    stockProfitEvent.Id, 
                    typeof(Program).Assembly.GetName().Name,
                    stockProfitEvent);

                _eventBus.Publish(stockProfitEvent);
            }
        }

        /// <summary>
        /// Возвращает список всех идентификаторов строк таблицы stock.StockProfit.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<int> GetAllStockProfitId()
        {
            IEnumerable<int> stockProfitIds = new List<int>();

            using (IDbConnection conn = new NpgsqlConnection(_options.ConnectionString))
            {
                try
                {
                    conn.Open();
                    stockProfitIds = conn.Query<int>(@"SELECT Id FROM [stock].[StockProfit]");
                }
                catch (NpgsqlException exception)
                {
                    _logger.LogCritical(exception, "FATAL ERROR: Database connections could not be opened: {Message}", exception.Message);
                }

            }

            return stockProfitIds;
        }
    }
}
