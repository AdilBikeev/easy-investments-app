using Stock.Domain.Events;

namespace Stock.API.Profiles
{
    public class StockProfitProfile : Profile
    {
        public StockProfitProfile()
        {
            // Source -> Target
            CreateMap<StockProfitStartedDomainEvent,
                      Domain.AggregatesModel.StockAggregate.StockProfit>();
        }
    }
}
