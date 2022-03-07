using System.Data;

using Dapper;

using Npgsql;

using Quotation.BackgroundTasks.Configurations;
using Quotation.BackgroundTasks.Events;
using Quotation.BuildingBlocks.EventBus.Abstractions;

namespace Quotation.BackgroundTasks.Services
{
    public class QuotationPorfitManagerService : BackgroundService
    {
        private readonly BackgroundTaskOptions _options;
        private readonly IEventBus _eventBus;
        private readonly ILogger<QuotationPorfitManagerService> _logger;

        public QuotationPorfitManagerService(
            IOptions<BackgroundTaskOptions> options, 
            IEventBus eventBus, 
            ILogger<QuotationPorfitManagerService> logger)
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
                SendAllQuotationProfitIds();

                await Task.Delay(_options.CheckUpdateTime, stoppingToken);
            }

            _logger.LogDebug($"{this}  background task is stopping.");
        }

        /// <summary>
        /// Помещает в шину данных Id сущностей таблицы Quotation.QuotationPorfit.
        /// </summary>
        private void SendAllQuotationProfitIds()
        {
            _logger.LogDebug($"Start call {nameof(GetAllQuotationProfitId)}");

            var orderIds = GetAllQuotationProfitId();

            foreach (var orderId in orderIds)
            {
                var QuotationProfitEvent = new QuotationProfitIntegrationEvent(orderId);

                _logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", 
                    QuotationProfitEvent.Id, 
                    typeof(Program).Assembly.GetName().Name,
                    QuotationProfitEvent);

                _eventBus.Publish(QuotationProfitEvent);
            }
        }

        /// <summary>
        /// Возвращает список всех идентификаторов строк таблицы Quotation.QuotationProfit.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<int> GetAllQuotationProfitId()
        {
            IEnumerable<int> QuotationProfitIds = new List<int>();

            using (IDbConnection conn = new NpgsqlConnection(_options.ConnectionString))
            {
                try
                {
                    conn.Open();
                    QuotationProfitIds = conn.Query<int>(@"SELECT Id FROM [Quotation].[QuotationProfit]");
                }
                catch (NpgsqlException exception)
                {
                    _logger.LogCritical(exception, "FATAL ERROR: Database connections could not be opened: {Message}", exception.Message);
                }

            }

            return QuotationProfitIds;
        }
    }
}
