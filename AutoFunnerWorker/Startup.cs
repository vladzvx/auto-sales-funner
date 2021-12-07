using Common.Interfaces;
using Common.Services;
using Common.Services.PeriodicWorkers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFunnerWorker
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddControllers();
            services.AddSingleton<HeadersProcessor>();
            services.AddSingleton<NewDealsSettings>();
            services.AddSingleton<CheckerSettings>();
            services.AddHostedService<PeriodicWorker<NewDealsSettings>>();
            services.AddHostedService<PeriodicWorker<CheckerSettings>>();
            services.AddDbContextFactory<ContactsContext>(
                options => 
                {
                    options.UseNpgsql(Environment.GetEnvironmentVariable("ConnectionString"));
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
