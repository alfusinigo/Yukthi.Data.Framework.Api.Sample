using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            //Add Sql Server
            services.AddSqlDatabase(Configuration);
                      
            //This can be either memory, redis or sql server (implementation of IDistributedCache)
            services.AddDistributedMemoryCache();

            //To Enable Yc framework Caching
            services.AddDataCaching(Configuration); 

            //Add specific Yc data controller and mappers
            services.AddDataControllerAndMapper<IEmployeeDataMapper, EmployeeDataMapper, IEmployeeDataController, EmployeeDataController>();
            
            //Add data repository
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddSwaggerGen(sg => { sg.SwaggerDoc("api", new Info { Title = "Employee Sample API", Version = "1" }); });
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
                si.SwaggerEndpoint("/swagger/employeesample.json", "Employee Sample API");
            });

            app.UseMvc();
        }
    }
}
