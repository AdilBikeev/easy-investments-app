﻿using Microsoft.EntityFrameworkCore;

using Quotation.API.Configuration;
using Quotation.API.Infrastracture.Filters;
using Quotation.API.SyncDataServices.Grps;
using Quotation.API.SyncDataServices.Soap;
using Quotation.BuildingBlocks.Database.Abstractions;
using Quotation.BuildingBlocks.Database.Extensions;
using Quotation.Domain.AggregatesModel.QuotationAggregate;
using Quotation.Domain.AggregatesModel.QuotationProfitAggregate;
using Quotation.Infrastructure;
using Quotation.Infrastructure.Repositories;
using Quotation.Infrastructure.Seeding;

namespace Quotation.API.Infrastracture.Extensions
{
    /// <summary>
    /// Расширение для разделения логики в Startup.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        private static string? AssemblyName => Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>
        /// Инжектирует фильтрацию, насройку роутинга, версионности и swagger для контроллеров.
        /// </summary>
        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(config =>
                    {
                        config.Filters.Add<ExceptionFilter>();
                    })
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    });
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddApiVersioning(options =>
                    {
                        // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                        options.ReportApiVersions = true;
                    });
            services.AddVersionedApiExplorer(options =>
                    {
                        // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                        // note: the specified format code will format the version as "'v'major[.minor][-status]"
                        options.GroupNameFormat = "'v'VVV";

                        // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                        // can also be used to control the format of the API version in route templates
                        options.SubstituteApiVersionInUrl = true;
                    });

            services.AddSwaggerGen(options =>
            {
                if (string.IsNullOrEmpty(AssemblyName))
                    throw new InvalidOperationException($"{nameof(AssemblyName)} cannot be null");

                var filePath = Path.Combine(System.AppContext.BaseDirectory, $"{AssemblyName}.xml");

                options.IncludeXmlComments(filePath, includeControllerXmlComments: true);
            })
            .AddSwaggerGenNewtonsoftSupport();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            return services;
        }

        /// <summary>
        /// Инжектируте Http сервисы вместе и подтягивает конфигурацию для них включая URL.
        /// </summary>
        public static IServiceCollection AddHttpServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            services.AddTransient<IQuotationDataClient, QuotationDataClient>();
            services.AddTransient<ICentralBankService, CentralBankService>();
            services.AddTransient<IValuteDataService, ValuteDataService>();

            return services;
        }

        /// <summary>
        /// Инжектирует подключения к БД.
        /// </summary>
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            //services.RegisterDbContextFactory<QuotationContext, MigrationHistoryRepository>(configuration, "DefaultConnection");
            services.AddDbContext<QuotationContext>(options =>
            {
                options.UseNpgsql(configuration["ConnectionStrings:DefaultConnection"],
                    npgsqlOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
                    });
            },
            ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
        );

            // Obsolete - подгрузка с CSV файла уже не нужна
            //services.AddSingleton<IFileSystemAccessor, FileSystemAccessor>();
            //services.AddScoped<IDbSeeder, QuotationDbSeeder>();

            return services;
        }
    }
}
