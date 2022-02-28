using Autofac.Extensions.DependencyInjection;

using Stock.API.Infrastracture.AutofacModules;
using Stock.API.Infrastracture.Extensions;

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
    public virtual IServiceProvider ConfigureServices(IServiceCollection services)
    {
        services.AddGrpc(options =>
        {
            options.EnableDetailedErrors = true;
        });

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddCustomMvc(Configuration)
                .AddInvestApiClient((_, settings) =>
                    {
                        settings.AccessToken = Configuration[nameof(settings.AccessToken)];
                    }
                )
                .AddHttpServices(Configuration);

        //configure autofac
        var container = new ContainerBuilder();
        container.Populate(services);

        container.RegisterModule(new MediatorModule());
        container.RegisterModule(new ApplicationModule(Configuration["ConnectionString"]));

        return new AutofacServiceProvider(container.Build());
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

