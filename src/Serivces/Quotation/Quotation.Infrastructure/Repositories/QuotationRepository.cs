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
        public async Task<QuotationAggregate.Quotation?> FindByFigiAsync(string figi)
        {
            var quotation = await _context.Quotation
                .Where(s => s.FIGI.Equals(figi))
                .AsNoTracking()
                .SingleOrDefaultAsync();
            return quotation;
        }

        /// <inheritdoc/>
        public async Task<QuotationAggregate.Quotation> AddOrUpdateAsync(QuotationAggregate.Quotation quotation)
        {
            var quotationEntity = quotation.Id != default(int) ?
                quotation :
                await FindByFigiAsync(quotation.FIGI);

            var quotationExist = quotationEntity is not null;

            if (quotationExist)
            {
                var quotationUpdate = quotationEntity!.CopyTo(quotation);
                _context.Entry(quotationEntity).CurrentValues.SetValues(quotationUpdate);
                _context.Quotation.Update(quotationEntity);
                return quotationEntity;
            } 
            else
            {
                return (await _context.Quotation.AddAsync(quotation)).Entity;
            }
        }
    }
}
