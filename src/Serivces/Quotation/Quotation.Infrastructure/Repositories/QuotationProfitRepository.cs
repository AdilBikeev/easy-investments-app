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
        public async Task<QuotationProfit?> FindByQuotationId(int quotationId)
        {
            var QuotationProfit = await _context.QuotationProfit
                .Where(s => s.QuotationId.Equals(quotationId))
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return QuotationProfit;
        }

        /// <inheritdoc/>
        public QuotationProfit AddOrUpdate(QuotationProfit quotationProfit)
        {
            try
            {
                var quotationProfitEntity = quotationProfit.Id != default(int) ?
                    quotationProfit :
                    FindByQuotationId(quotationProfit.QuotationId).Result;

                var quotationExist = quotationProfitEntity is not null;

                if (quotationExist)
                {
                    //TODO: возможно нужно делать копирование Id в quotationProfit по примеру с Quotation.CopyTo
                    _context.Entry(quotationProfitEntity!).CurrentValues.SetValues(quotationProfit);
                    _context.QuotationProfit.Update(quotationProfitEntity!);
                    return quotationProfitEntity!;
                }
                else
                {
                    return _context.QuotationProfit.Add(quotationProfit).Entity;
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            return null;
        }
    }
}
