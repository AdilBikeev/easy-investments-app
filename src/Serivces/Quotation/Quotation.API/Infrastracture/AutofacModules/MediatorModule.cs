

using Quotation.API.Application.DomainEventHandlers;

namespace Quotation.API.Infrastracture.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        /// <summary>
        /// Реистрирует типы обработчиков событий в контейнере IOC.
        /// </summary>
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            //    .AsImplementedInterfaces();

            //// Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
            //builder.RegisterAssemblyTypes(typeof(ValidateOrAddQuotationAggregateWhenQuotationProfitStartedDomainEventHandler).GetTypeInfo().Assembly)
            //    .AsClosedTypesOf(typeof(INotificationHandler<>));

            //builder.Register<ServiceFactory>(context =>
            //{
            //    var componentContext = context.Resolve<IComponentContext>();
            //    return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            //});
        }
    }
}
