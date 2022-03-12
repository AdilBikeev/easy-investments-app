using Quotation.BuildingBlocks.Database.Abstractions;
using Quotation.Domain.AggregatesModel.QuotationProfitAggregate;
using QuotationAggregate = Quotation.Domain.AggregatesModel.QuotationAggregate;
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
        public DbSet<QuotationProfit> QuotationProfit => Set<QuotationProfit>();
        public DbSet<QuotationAggregate.Quotation> Quotation => Set<QuotationAggregate.Quotation>();

        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        /// <summary>
        /// Для операция с EF CLI.
        /// </summary>
        //public QuotationContext(DbContextOptions<QuotationContext> options) : base(options) { }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        public string SchemaName => DEFAULT_SCHEMA;

        /// <summary>
        /// Для запуска приложения.
        /// </summary>
        public QuotationContext(DbContextOptions<QuotationContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        
        
            System.Diagnostics.Debug.WriteLine($"{nameof(QuotationContext)}::ctor ->" + this.GetHashCode());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
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
            modelBuilder.ApplyConfiguration(new QuotationEntityTypeConfiguration());
        }

        public void Migrate()
        {
            Database.Migrate();
        }
    }
}
