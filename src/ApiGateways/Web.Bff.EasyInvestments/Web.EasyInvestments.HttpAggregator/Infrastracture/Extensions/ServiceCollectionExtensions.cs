﻿using System.Reflection;

using EasyInvestments.Quotation.API.V1;

using Newtonsoft.Json.Converters;

using Web.EasyInvestments.HttpAggregator.Configuration;

namespace Web.EasyInvestments.HttpAggregator.Infrastracture.Extensions
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
            services.AddControllers()
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
            var urlsConf = configuration.GetSection("urls").Get<UrlsConfig>();

            services.AddHttpClient<IQuotationClient, QuotationClient>(client =>
                    client.BaseAddress = new Uri(urlsConf.Quotation));

            return services;
        }
    }
}
