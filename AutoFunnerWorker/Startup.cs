using Common.Interfaces;
using Common.Services;
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
            services.AddControllers();
            services.AddTransient<ISettings, ThreadSafeSettings>();
            services.AddHostedService<SalesCreator>();
            //services.AddHostedService<LinksClickChecker>();
            services.AddDbContextFactory<ContactsContext>(
                options => 
                {
                    string cnnstr = Environment.GetEnvironmentVariable("ConnectionString");
                    options.UseNpgsql(cnnstr);
                    //try
                    //{
                    //    var databaseCreator = options.GetService(typeof(IDatabaseCreator));
                    //    databaseCreator.CreateTables();
                    //}
                    //catch (Exception ex)
                    //{
                    //    //A SqlException will be thrown if tables already exist. So simply ignore it.
                    //}
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
