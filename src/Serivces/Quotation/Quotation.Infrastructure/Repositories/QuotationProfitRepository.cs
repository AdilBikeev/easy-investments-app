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
        public async Task<QuotationProfit?> FindByQuotationIdAsync(int quotationId)
        {
            var QuotationProfit = await _context.QuotationProfit
                .Where(s => s.QuotationId.Equals(quotationId))
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return QuotationProfit;
        }

        /// <inheritdoc/>
        public async Task<QuotationProfit> AddOrUpdateAsync(QuotationProfit quotationProfit, int quotationId)
        {
            var quotationProfitEntity = quotationProfit.Id != default(int) ?
                quotationProfit :
                await FindByQuotationIdAsync(quotationId);

            var quotationExist = quotationProfitEntity is not null;

            if (quotationExist)
            {
                var quotationProfitUpdate = quotationProfitEntity!.CopyTo(quotationProfit, quotationId);
                _context.Entry(quotationProfitEntity!).CurrentValues.SetValues(quotationProfitUpdate);
                _context.QuotationProfit.Update(quotationProfitEntity!).State = EntityState.Modified;
                return quotationProfitEntity!;
            }
            else
            {
                return (await _context.QuotationProfit.AddAsync(quotationProfit)).Entity;
            }
        }
    }
}
