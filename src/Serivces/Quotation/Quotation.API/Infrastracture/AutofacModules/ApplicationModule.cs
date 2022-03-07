﻿using Quotation.BuildingBlocks.EventBus.Abstractions;
using Quotation.Domain.AggregatesModel.QuotationAggregate;
using Quotation.Infrastructure.Repositories;

namespace Quotation.API.Infrastracture.AutofacModules
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

            builder.RegisterType<QuotationRepository>()
                .As<IQuotationProfitRepository>()
                .InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(typeof(CreateQuotationProfitCommandHandler).GetTypeInfo().Assembly)
            //    .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }
    }
}