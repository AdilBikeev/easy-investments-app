using Quotation.Domain.AggregatesModel.QuotationProfitAggregate;
using Quotation.Domain.Events;

namespace Quotation.API.Application.DomainEventHandlers
{
    /// <summary>
    /// Обработчик события <see cref="QuotationProfitStartedDomainEvent"/>.
    /// </summary>
    public class ValidateOrAddQuotationAggregateWhenQuotationProfitStartedDomainEventHandler : INotificationHandler<QuotationProfitStartedDomainEvent>
    {
        private readonly ILoggerFactory _logger;
        private IMapper _mapper;
        private readonly IQuotationProfitRepository _QuotationProfitRepository;

        public ValidateOrAddQuotationAggregateWhenQuotationProfitStartedDomainEventHandler(
    ILoggerFactory logger,
    IMapper mapper,
    IQuotationProfitRepository QuotationProfitRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _QuotationProfitRepository = QuotationProfitRepository;
        }

        public async Task Handle(QuotationProfitStartedDomainEvent QuotationProfitStartedEvent, CancellationToken cancellationToken)
        {
            var figi = QuotationProfitStartedEvent.FIGI;
            var QuotationProfit = await _QuotationProfitRepository.FindAsync(figi);
            
            bool QuotationProfitOriginallyExisted = QuotationProfit is not null;

            if (!QuotationProfitOriginallyExisted)
            {
                QuotationProfit = _mapper.Map<QuotationProfit>(QuotationProfitStartedEvent);
            }

            var QuotationProfitUpdated = QuotationProfitOriginallyExisted ? _QuotationProfitRepository.Update(QuotationProfit)
                                                                          : _QuotationProfitRepository.Add(QuotationProfit);
            
            await _QuotationProfitRepository.UnitOfWork
                                        .SaveEntitiesAsync();

            _logger.CreateLogger<ValidateOrAddQuotationAggregateWhenQuotationProfitStartedDomainEventHandler>()
            .LogTrace($"QuotationProfit {QuotationProfitUpdated.Id} and related payment method were validated or updated for FIGI: {figi}.");
        }
    }
}
