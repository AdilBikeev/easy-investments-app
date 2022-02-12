
using Microsoft.AspNetCore.Mvc.ApiExplorer;

using Web.EasyInvestments.HttpAggregator.Infrastracture.Extensions;

namespace Web.EasyInvestments.HttpAggregator
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
            app.UseSwagger()
               .UseSwaggerUI(setup =>
               {
                   foreach (var description in provider.ApiVersionDescriptions)
                   {
                       setup.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                   }
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc(Configuration)
                    .AddHttpServices(Configuration);
        }
    }
}