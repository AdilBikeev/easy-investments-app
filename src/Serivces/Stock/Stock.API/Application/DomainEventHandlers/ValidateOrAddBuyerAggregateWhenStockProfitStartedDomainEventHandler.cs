using Stock.Domain.AggregatesModel.StockAggregate;
using Stock.Domain.Events;

namespace Stock.API.Application.DomainEventHandlers
{
    /// <summary>
    /// Обработчик события <see cref="StockProfitStartedDomainEvent"/>.
    /// </summary>
    public class ValidateOrAddStockAggregateWhenStockProfitStartedDomainEventHandler : INotificationHandler<StockProfitStartedDomainEvent>
    {
        private readonly ILoggerFactory _logger;
        private IMapper _mapper;
        private readonly IStockProfitRepository _stockProfitRepository;

        public ValidateOrAddStockAggregateWhenStockProfitStartedDomainEventHandler(
    ILoggerFactory logger,
    IMapper mapper,
    IStockProfitRepository stockProfitRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _stockProfitRepository = stockProfitRepository;
        }

        public async Task Handle(StockProfitStartedDomainEvent stockProfitStartedEvent, CancellationToken cancellationToken)
        {
            var figi = stockProfitStartedEvent.FIGI;
            var stockProfit = await _stockProfitRepository.FindAsync(figi);
            
            bool stockProfitOriginallyExisted = stockProfit is not null;

            if (!stockProfitOriginallyExisted)
            {
                stockProfit = new StockProfit(_mapper.Map<StockProfit>(stockProfitStartedEvent));
            }

            var stockProfitUpdated = stockProfitOriginallyExisted ? _stockProfitRepository.Update(stockProfit)
                                                                          : _stockProfitRepository.Add(stockProfit);
            
            await _stockProfitRepository.UnitOfWork
                                        .SaveEntitiesAsync();

            _logger.CreateLogger<ValidateOrAddStockAggregateWhenStockProfitStartedDomainEventHandler>()
            .LogTrace($"StockProfit {stockProfitUpdated.Id} and related payment method were validated or updated for orderId: {stockProfitUpdated.FIGI}.");
        }
    }
}
