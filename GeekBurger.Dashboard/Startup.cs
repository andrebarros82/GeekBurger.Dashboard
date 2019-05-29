using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GeekBurger.Dashboard.Repository;
using GeekBurger.Dashboard.Repository.DataContext;
using GeekBurger.Dashboard.Repository.DataContext.Extensions;
using GeekBurger.Dashboard.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GeekBurger.Dashboard
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            IMvcCoreBuilder mvcCoreBuilder = services.AddMvcCore();

            mvcCoreBuilder.AddFormatterMappings().AddJsonFormatters().AddCors();


            services.AddDbContext<DashboardContext>(o => o.UseInMemoryDatabase("geekburger-dashboard"));
            services.AddScoped<ISalesRepository, SalesRepository>();


            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DashboardContext dashboardContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });

            dashboardContext.Seed();
        }
    }
}
