using QuotationAggregate = Quotation.Domain.AggregatesModel.QuotationAggregate;
using Quotation.Domain.SeedWork;

namespace Quotation.Infrastructure.Repositories
{
    public class QuotationRepository : QuotationAggregate.IQuotationRepository
    {
        private readonly QuotationContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public QuotationRepository(QuotationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public QuotationAggregate.Quotation Add(QuotationAggregate.Quotation quotation)
        {
            return _context.Quotation.Add(quotation).Entity;
        }

        /// <inheritdoc/>
        public async Task<QuotationAggregate.Quotation> FindByFIGIAsync(string figi)
        {
            var quotation = await _context.Quotation
                .Where(s => s.FIGI.Equals(figi))
                .SingleOrDefaultAsync();

            return quotation;
        }

        /// <inheritdoc/>
        public QuotationAggregate.Quotation Update(QuotationAggregate.Quotation QuotationProfit)
        {
            return _context.Quotation
                    .Update(QuotationProfit)
                    .Entity;
        }
    }
}
