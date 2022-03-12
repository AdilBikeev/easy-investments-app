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
        public async Task<QuotationProfit> Add(QuotationProfit QuotationProfit)
        {
            return (await _context.QuotationProfit.AddAsync(QuotationProfit)).Entity;
        }

        /// <inheritdoc/>
        public async Task<QuotationProfit?> FindByQuotationId(int quotationId)
        {
            var QuotationProfit = await _context.QuotationProfit
                .Where(s => s.QuotationId.Equals(quotationId))
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
