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

        /// <inheritdoc/>
        public async Task<StockProfit> FindAsync(string figi)
        {
            var stockProfit = await _context.StockProfit
                .Where(s => s.FIGI.Equals(figi))
                .SingleOrDefaultAsync();

            return stockProfit;
        }

        /// <inheritdoc/>
        public StockProfit Update(StockProfit stockProfit)
        {
            return _context.StockProfit
                    .Update(stockProfit)
                    .Entity;
        }
    }
}
