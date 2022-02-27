using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Mvc.ApiExplorer;

using Stock.BackgroundTasks.Configurations;
using Stock.BackgroundTasks.Infrastracture.Extension;
using Stock.BackgroundTasks.Services;

namespace Stock.BackgroundTasks
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
        IApplicationBuilder app,
        IWebHostEnvironment env,
        IApiVersionDescriptionProvider provider)
        {
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc(Configuration)
                    .Configure<BackgroundTaskOptions>(Configuration)
                    .AddOptions()
                    .AddHostedService<StockPorfitManagerService>()
                    .AddHttpServices(Configuration)
                    .AddEventBus(Configuration);
        }
    }
}
