﻿using QuotationAggregate = Quotation.Domain.AggregatesModel.QuotationAggregate;
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
            var quotationModel = _mapper.Map<QuotationAggregate.Quotation>(request);
            
            var quotationUpdate = await _quotationRepository.AddOrUpdateAsync(quotationModel);

            //TODO: Исправить логику вызова SaveEntities, чтобы при 1 запроса пользвоателя не было несколько вызовов SaveEntities.
            // Можно как-то через DomainEvents организовать
            return await _quotationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
