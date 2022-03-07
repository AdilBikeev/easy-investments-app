using Quotation.API.DTOs;

namespace Quotation.API.Profiles
{
    public class MoneyValueProfile : Profile
    {
        public MoneyValueProfile()
        {
            // Source -> Target
            CreateMap<Tinkoff.InvestApi.V1.Quotation, MoneyValueDTO>();
            CreateMap<Tinkoff.InvestApi.V1.MoneyValue, MoneyValueDTO>();
        }
    }
}
