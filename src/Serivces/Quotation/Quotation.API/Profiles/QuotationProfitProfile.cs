using Quotation.API.Application.Commands;
using Quotation.Domain.AggregatesModel.QuotationProfitAggregate;
using Quotation.Domain.Events;

namespace Quotation.API.Profiles
{
    public class QuotationProfitProfile : Profile
    {
        public QuotationProfitProfile()
        {
            // Source -> Target
            CreateMap<QuotationProfitStartedDomainEvent, QuotationProfit>();
            CreateMap<CreateOrUpdateQuotationProfitCommand, QuotationProfit>();
        }
    }
}
