using Quotation.Domain.AggregatesModel.QuotationProfitAggregate;
using Quotation.Domain.SeedWork;

namespace Quotation.Infrastructure.Repositories
{
    public class QuotationProfitRepository : IQuotationProfitRepository
    {
        private readonly QuotationContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public QuotationProfitRepository(QuotationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public QuotationProfit Add(QuotationProfit QuotationProfit)
        {
            return _context.QuotationProfit.Add(QuotationProfit).Entity;
        }

        /// <inheritdoc/>
        public async Task<QuotationProfit> FindAsync(string figi)
        {
            var QuotationProfit = await _context.QuotationProfit
                .Where(s => s.FIGI.Equals(figi))
                .SingleOrDefaultAsync();

            return QuotationProfit;
        }

        /// <inheritdoc/>
        public QuotationProfit Update(QuotationProfit QuotationProfit)
        {
            return _context.QuotationProfit
                    .Update(QuotationProfit)
                    .Entity;
        }
    }
}
