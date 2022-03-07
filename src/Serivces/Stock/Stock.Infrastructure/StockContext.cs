using Microsoft.EntityFrameworkCore.Storage;

using Stock.BuildingBlocks.Database.Abstractions;
using Stock.Domain.AggregatesModel.StockAggregate;
using Stock.Domain.SeedWork;
using Stock.Infrastructure.EntityConfigurations;

namespace Stock.Infrastructure
{
    /// <summary>
    /// Описание БД котировок.
    /// </summary>
    public class StockContext : DbContext, IMigratoryDbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "stock";
        public DbSet<StockProfit> StockProfit { get; set; }

        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        public StockContext(DbContextOptions<StockContext> options) : base(options) { }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        public string SchemaName => DEFAULT_SCHEMA;

        public StockContext(DbContextOptions<StockContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


            System.Diagnostics.Debug.WriteLine($"{nameof(StockContext)}::ctor ->" + this.GetHashCode());
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
            modelBuilder.ApplyConfiguration(new StockProfitEntityTypeConfiguration());
        }

        public void Migrate()
        {
            Database.Migrate();
        }
    }
}
