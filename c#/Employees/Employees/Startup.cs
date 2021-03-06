using Employees.Interfaces;
using Employees.Database;
using Employees.DataService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Employees.Lib;

namespace Employees
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
            services.AddControllers();

            // Add EmployeeDatabase to services (DI)

            var config = this.Configuration.GetSection("Employee").Get<EmployeeConfig>();

            services.AddDbContext<EmployeeDbContext>(options => options.UseInMemoryDatabase(databaseName: "Employees"));

            if (config.UseInMemoryDb)
            {
                services.AddScoped<IEmployeeService, EmployeeService>();

            }
            else
            {
                var db = new JsonEmployeeService();
                services.AddSingleton<IEmployeeService>(db);
                services.AddHostedService<DatabaseiInitializerService>();
            }

            // Swagger
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Employees API";
                    document.Info.Description = "A RESTful microservice to manage employees";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Drew Bentley",
                        Email = string.Empty,
                      
                    };
                  
                };
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           

            app.UseHttpsRedirection();

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
