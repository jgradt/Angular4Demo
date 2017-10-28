using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApiDemo.Data;
using AutoMapper;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.EntityFrameworkCore;

namespace WebApiDemo
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
            services.AddCors();

            services.AddDbContext<DemoDbContext>(opt => opt.UseInMemoryDatabase("demoDb"));

            services.AddMvc();

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddAutoMapper();

            // see: https://elanderson.net/2017/10/swagger-and-swashbuckle-with-asp-net-core-2/
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "WebApiDemo API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                );

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiDemo V1");
            });

            // seed database with data
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<DemoDbContext>();
                DatabaseInitializer.AddDatabaseSeedData(dbContext);
            }
            
        }
    }
}
