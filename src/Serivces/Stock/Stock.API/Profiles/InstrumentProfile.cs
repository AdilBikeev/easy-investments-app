namespace Stock.API.Profiles
{
    public class InstrumentProfile : Profile
    {
        public InstrumentProfile()
        {
            // Source -> Target
            CreateMap<Tinkoff.InvestApi.V1.Instrument, InstrumentDTO>()
                .ForMember(
                dest => dest.Currency, 
                opt => opt.MapFrom(
                    src => ValuteConverterHelper.CurrencyCodeDict[src.Currency.ToLower()]
                    )
                );
        }
    }
}
