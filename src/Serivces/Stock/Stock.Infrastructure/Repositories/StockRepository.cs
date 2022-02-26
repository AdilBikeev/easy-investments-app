using Stock.Domain.AggregatesModel.StockAggregate;
using Stock.Domain.SeedWork;

namespace Stock.Infrastructure.Repositories
{
    public class StockRepository : IStockProfitRepository
    {
        private readonly StockContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public StockRepository(StockContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public StockProfit Add(StockProfit stockProfit)
        {
            return _context.StockProfit.Add(stockProfit).Entity;
        }
    }
}
