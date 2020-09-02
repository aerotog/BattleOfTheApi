using JsonApiDotNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BOTA.API.REST.JsonApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ShopContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ShopContext")));

            // AddJsonApi replaces AddControllers
            services.AddJsonApi<ShopContext>(
                options =>
                {
                    options.Namespace = "api";
                    options.IncludeExceptionStackTraceInErrors = true;
                    options.DefaultPageSize = 4;
                    options.IncludeTotalRecordCount = true;
                    options.ValidateModelState = true;
                },
                discovery => discovery.AddCurrentAssembly());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // New call for JsonApi
            app.UseJsonApi();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
