using FluentValidation.AspNetCore;
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

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = $"easy-investments-app - {nameof(Stock)} HTTP API",
                Version = "v1",
                Description = "The Stock Service HTTP API"
            });

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
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        var pathBase = Configuration["PATH_BASE"];
        if (!string.IsNullOrEmpty(pathBase))
        {
            app.UsePathBase(pathBase);
        }

        app.UseSwagger()
            .UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", $"{nameof(Stock)}.API V1");
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

