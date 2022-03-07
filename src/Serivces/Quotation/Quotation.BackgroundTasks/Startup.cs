using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Mvc.ApiExplorer;

using Quotation.BackgroundTasks.Configurations;
using Quotation.BackgroundTasks.Infrastracture.Extension;
using Quotation.BackgroundTasks.Services;

namespace Quotation.BackgroundTasks
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
                    .AddHostedService<QuotationPorfitManagerService>()
                    .AddHttpServices(Configuration)
                    .AddEventBus(Configuration);
        }
    }
}
