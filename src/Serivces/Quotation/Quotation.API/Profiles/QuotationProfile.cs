using Quotation.API.Application.Commands;
using Quotation.Domain.AggregatesModel.QuotationProfitAggregate;

namespace Quotation.API.Profiles
{
    public class QuotationProfile : Profile
    {
        public QuotationProfile()
        {
            // Source -> Target
            CreateMap<CreateQuotationCommand, QuotationProfit>();
        }
    }
}
