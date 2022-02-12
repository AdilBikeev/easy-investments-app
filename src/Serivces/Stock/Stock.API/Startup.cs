using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Mvc.ApiExplorer;

using Newtonsoft.Json.Converters;

using Stock.API.Configuration;
using Stock.API.Controllers;
using Stock.API.SyncDataServices.Grps;

using System.Reflection;

namespace Stock.API;

public class Startup
{
    private static string? AssemblyName => Assembly.GetExecutingAssembly().GetName().Name;

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddGrpc(options =>
        {
            options.EnableDetailedErrors = true;
        });

        services.AddControllers()
            .AddApplicationPart(typeof(StockController).Assembly)
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddApiVersioning(
            options =>
            {
                // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                options.ReportApiVersions = true;
            });

        services.AddVersionedApiExplorer(
                        options =>
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

        services.Configure<StockDataClientOptions>(Configuration.GetSection("StockDataClient"));
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddTransient<IStockDataClient, StockDataClient>();
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddOptions();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(
        IApplicationBuilder app,
        IWebHostEnvironment env,
        IApiVersionDescriptionProvider provider)
    {
        app.UseSwagger()
            .UseSwaggerUI(setup =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    setup.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
                setup.OAuthClientId($"{nameof(Stock)}swaggerui");
                setup.OAuthAppName($"{nameof(Stock)} Swagger UI");
            });

        app.UseRouting();
        app.UseCors("CorsPolicy");

        app.UseStaticFiles();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapControllers();
        });

        app.UseHttpsRedirection();
    }
}

