using Stock.BuildingBlocks.EventBus.Abstractions;
using Stock.Domain.AggregatesModel.StockAggregate;
using Stock.Infrastructure.Repositories;

namespace Stock.API.Infrastracture.AutofacModules
{
    public class ApplicationModule
        : Autofac.Module
    {
        /// <summary>
        /// Строка подключения к БД для запросов.
        /// </summary>
        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;
        }

        protected override void Load(ContainerBuilder builder)
        {
            //builder.Register(c => new OrderQueries(QueriesConnectionString))
            //    .As<IOrderQueries>()
            //    .InstancePerLifetimeScope();

            builder.RegisterType<StockRepository>()
                .As<IStockProfitRepository>()
                .InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(typeof(CreateStockProfitCommandHandler).GetTypeInfo().Assembly)
            //    .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }
    }
}
