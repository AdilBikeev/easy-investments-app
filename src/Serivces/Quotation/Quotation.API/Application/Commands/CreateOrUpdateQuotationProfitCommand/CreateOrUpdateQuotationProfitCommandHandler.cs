using QuotationAggregate = Quotation.Domain.AggregatesModel.QuotationAggregate;
using Quotation.Domain.AggregatesModel.QuotationProfitAggregate;

namespace Quotation.API.Application.Commands
{
    /// <summary>
    /// Обработчик команды <see cref="CreateOrUpdateQuotationProfitCommand"/>
    /// </summary>
    public class CreateOrUpdateQuotationProfitCommandHandler
        : IRequestHandler<CreateOrUpdateQuotationProfitCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IQuotationProfitRepository _quotationProfitRepository;
        private readonly QuotationAggregate.IQuotationRepository _quotationRepository;
        private readonly ILogger<CreateOrUpdateQuotationProfitCommandHandler> _logger;

        public CreateOrUpdateQuotationProfitCommandHandler(
            IMediator mediator,
            IMapper mapper,
            IQuotationProfitRepository quotationProfitRepository,
            QuotationAggregate.IQuotationRepository quotationRepository,
            ILogger<CreateOrUpdateQuotationProfitCommandHandler> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mediator));
            _quotationProfitRepository = quotationProfitRepository ?? throw new ArgumentNullException(nameof(quotationProfitRepository));
            _quotationRepository = quotationRepository ?? throw new ArgumentNullException(nameof(quotationRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CreateOrUpdateQuotationProfitCommand request, CancellationToken cancellationToken)
        {
            var quotation = await _quotationRepository.FindByFigi(request.FIGI);
            if (quotation is null)
                throw new ArgumentOutOfRangeException(nameof(request.FIGI));

            var quotationProfitModel = _mapper.Map<QuotationProfit>(request);
            var quotatipnCopyModel = quotationProfitModel.CopyTo(quotationProfitModel, quotationProfitModel.QuotationId);

            var quotationProfitUpdate = _quotationProfitRepository.AddOrUpdate(quotatipnCopyModel);

            return await _quotationProfitRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
