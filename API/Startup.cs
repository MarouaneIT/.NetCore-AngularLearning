using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<StoreContext>(x =>
            {
                x.UseSqlite(_config.GetConnectionString("DefaultConnection"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<StoreContext>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Startup>>();
                var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
                try
                {
                    await dbContext.Database.MigrateAsync();                 
                    await SeedDataContext.SeedAsync(scope.ServiceProvider.GetRequiredService<StoreContext>(),
                        loggerFactory);
                }
                catch(Exception ex)
                {
                    logger.LogError(ex, "Error logged during migration");
                }
            }
        }
    }
}
