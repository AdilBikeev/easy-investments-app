using Quotation.Domain.SeedWork;

namespace Quotation.Infrastructure
{
    static class MediatorExtension
    {
        /// <summary>
        /// Dispatch Domain Events collection. 
        /// Choices:
        /// A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        /// side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        /// B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        /// You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers.  
        /// </summary>
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, QuotationContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
