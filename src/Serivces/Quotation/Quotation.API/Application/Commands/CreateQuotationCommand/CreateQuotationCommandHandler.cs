using QuotationAggregate = Quotation.Domain.AggregatesModel.QuotationAggregate;
using Quotation.Domain.AggregatesModel.QuotationProfitAggregate;

namespace Quotation.API.Application.Commands
{
    /// <summary>
    /// Обработчик команды <see cref="CreateQuotationCommand"/>
    /// </summary>
    public class CreateQuotationCommandHandler
        : IRequestHandler<CreateQuotationCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly QuotationAggregate.IQuotationRepository _quotationRepository;
        private readonly ILogger<CreateQuotationCommandHandler> _logger;

        public CreateQuotationCommandHandler(
            IMediator mediator,
            IMapper mapper,
            QuotationAggregate.IQuotationRepository quotationRepository,
            ILogger<CreateQuotationCommandHandler> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mediator));
            _quotationRepository = quotationRepository ?? throw new ArgumentNullException(nameof(quotationRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CreateQuotationCommand request, CancellationToken cancellationToken)
        {
            var quotation = await _quotationRepository.FindByFigi(request.FIGI);
            if (quotation is not null)
                return true;

            var quotationModel = _mapper.Map<QuotationAggregate.Quotation>(request);
            _quotationRepository.Add(quotationModel);

            return await _quotationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
