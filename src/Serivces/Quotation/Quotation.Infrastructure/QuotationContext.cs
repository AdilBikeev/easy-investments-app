using Microsoft.EntityFrameworkCore.Storage;

using Quotation.BuildingBlocks.Database.Abstractions;
using Quotation.Domain.AggregatesModel.QuotationAggregate;
using Quotation.Domain.SeedWork;
using Quotation.Infrastructure.EntityConfigurations;

namespace Quotation.Infrastructure
{
    /// <summary>
    /// Описание БД котировок.
    /// </summary>
    public class QuotationContext : DbContext, IMigratoryDbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "quotation";
        public DbSet<QuotationProfit> QuotationProfit { get; set; }

        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        public QuotationContext(DbContextOptions<QuotationContext> options) : base(options) { }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        public string SchemaName => DEFAULT_SCHEMA;

        public QuotationContext(DbContextOptions<QuotationContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


            System.Diagnostics.Debug.WriteLine($"{nameof(QuotationContext)}::ctor ->" + this.GetHashCode());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        /// <summary>
        /// Формирует БД применяя конфигурации каждой из таблиц.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new QuotationProfitEntityTypeConfiguration());
        }

        public void Migrate()
        {
            Database.Migrate();
        }
    }
}
