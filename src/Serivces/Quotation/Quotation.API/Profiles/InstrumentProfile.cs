using Quotation.API.DTOs;

namespace Quotation.API.Profiles
{
    public class InstrumentProfile : Profile
    {
        public InstrumentProfile()
        {
            // Source -> Target
            CreateMap<Tinkoff.InvestApi.V1.Instrument, InstrumentDTO>();
        }
    }
}
