﻿using Autofac.Extensions.DependencyInjection;

using MediatR;
using Quotation.API.Infrastracture.AutofacModules;
using Quotation.API.Infrastracture.Extensions;
using Quotation.BuildingBlocks.Database;

namespace Quotation.API;

public class Startup
{
    private static string? AssemblyName => Assembly.GetExecutingAssembly().GetName().Name;

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    //public virtual IServiceProvider ConfigureServices(IServiceCollection services)
    public void ConfigureServices(IServiceCollection services)
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
                .AddCustomDbContext(Configuration)
                .AddHttpServices(Configuration)
                .AddAutofac();
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule(new MediatorModule());
        builder.RegisterModule(new ApplicationModule(Configuration["ConnectionStrings:DefaultConnection"]));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(
        IApplicationBuilder app,
        IWebHostEnvironment env,
        //DatabaseMigrator databaseMigrator,
        IApiVersionDescriptionProvider provider)
    {
        //TODO: Add migrations
        //databaseMigrator.Migrate();

        app.UseSwagger()
            .UseSwaggerUI(setup =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    setup.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
                setup.OAuthClientId($"{nameof(Quotation)}swaggerui");
                setup.OAuthAppName($"{nameof(Quotation)} Swagger UI");
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

