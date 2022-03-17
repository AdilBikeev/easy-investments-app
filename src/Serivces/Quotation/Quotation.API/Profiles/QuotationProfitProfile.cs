using Quotation.API.Application.Commands;
using Quotation.API.DTOs;
using Quotation.Domain.AggregatesModel.QuotationProfitAggregate;
using Quotation.Domain.Events;

namespace Quotation.API.Profiles
{
    public class QuotationProfitProfile : Profile
    {
        public QuotationProfitProfile()
        {
            // Source -> Destination
            CreateMap<QuotationProfitStartedDomainEvent, QuotationProfit>();
            CreateMap<CreateOrUpdateQuotationProfitCommand, QuotationProfit>();
        }
    }
}
