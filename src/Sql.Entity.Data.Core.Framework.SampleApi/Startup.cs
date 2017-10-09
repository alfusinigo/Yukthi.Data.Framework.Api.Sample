using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Yc.Sql.Entity.Data.Framework.Extensions;
using Sql.Entity.Data.Core.Framework.SampleApi.DataAccess.Mappers;
using Sql.Entity.Data.Core.Framework.SampleApi.DataAccess.Controllers;
using Swashbuckle.AspNetCore.Swagger;
using Sql.Entity.Data.Core.Framework.SampleApi.Repositories;

namespace Sql.Entity.Data.Core.Framework.SampleApi
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
            services.AddMvc();

            //Add Yc framework services
            services.AddSqlDatabase(Configuration);
            services.AddInternalCaching(Configuration);
            services.AddDataControllerAndMapper<IEmployeeDataMapper, EmployeeDataMapper, IEmployeeDataController, EmployeeDataController>();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddSwaggerGen(sg => { sg.SwaggerDoc("v1", new Info { Title = "Employee API", Version = "v1" }); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(si =>
            {
                si.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee API V1");
            });

            app.UseMvc();
        }
    }
}
