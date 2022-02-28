

using Stock.API.Application.DomainEventHandlers;

namespace Stock.API.Infrastracture.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        /// <summary>
        /// Реистрирует типы обработчиков событий в контейнере IOC.
        /// </summary>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            // Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
            builder.RegisterAssemblyTypes(typeof(ValidateOrAddStockAggregateWhenStockProfitStartedDomainEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>));

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });
        }
    }
}
