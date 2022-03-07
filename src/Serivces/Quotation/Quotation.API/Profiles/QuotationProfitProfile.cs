using Quotation.Domain.Events;

namespace Quotation.API.Profiles
{
    public class QuotationProfitProfile : Profile
    {
        public QuotationProfitProfile()
        {
            // Source -> Target
            CreateMap<QuotationProfitStartedDomainEvent,
                      Domain.AggregatesModel.QuotationAggregate.QuotationProfit>();
        }
    }
}
