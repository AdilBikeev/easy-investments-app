using QuotationAggregate = Quotation.Domain.AggregatesModel.QuotationAggregate;
using Quotation.Domain.AggregatesModel.QuotationProfitAggregate;

namespace Quotation.API.Application.Commands
{
    /// <summary>
    /// Обработчик команды <see cref="CreateOrUpdateQuotationCommand"/>
    /// </summary>
    public class CreateOrUpdateQuotationCommandHandler
        : IRequestHandler<CreateOrUpdateQuotationCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly QuotationAggregate.IQuotationRepository _quotationRepository;
        private readonly ILogger<CreateOrUpdateQuotationCommandHandler> _logger;

        public CreateOrUpdateQuotationCommandHandler(
            IMediator mediator,
            IMapper mapper,
            QuotationAggregate.IQuotationRepository quotationRepository,
            ILogger<CreateOrUpdateQuotationCommandHandler> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mediator));
            _quotationRepository = quotationRepository ?? throw new ArgumentNullException(nameof(quotationRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CreateOrUpdateQuotationCommand request, CancellationToken cancellationToken)
        {
            var quotation = await _quotationRepository.FindByFigi(request.FIGI);
            var quotationExisted = quotation is not null;

            var quotationModel = _mapper.Map<QuotationAggregate.Quotation>(request);
            
            var quotationUpdate = quotationExisted ? 
                _quotationRepository.Update(quotationModel) :
                _quotationRepository.Add(quotationModel).Result;

            return await _quotationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
