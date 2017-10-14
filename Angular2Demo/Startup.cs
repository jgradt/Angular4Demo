using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Angular2Demo.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Angular2Demo
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
            services.AddDbContext<DemoDbContext>(opt => opt.UseInMemoryDatabase("demoDb"));

            services.AddMvc();

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddAutoMapper();

            // see: https://elanderson.net/2017/10/swagger-and-swashbuckle-with-asp-net-core-2/
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Contacts API", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contacts API V1");
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DemoDbContext>();
                AddDatabaseSeedData(context);

                var d = context.Customers.ToList();
            }

            
        }

        private void AddDatabaseSeedData(DemoDbContext dbContext)
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Data.Entities.Customer() { Id = 1, FirstName = "Jimmy", LastName = "Buffett", CreatedDate = new DateTime(2000, 1, 1), LastUpdatedDate = DateTime.Today });
                dbContext.Customers.Add(new Data.Entities.Customer() { Id = 2, FirstName = "James", LastName = "Dean", CreatedDate = new DateTime(2000, 1, 1), LastUpdatedDate = DateTime.Today });
                dbContext.Customers.Add(new Data.Entities.Customer() { Id = 3, FirstName = "Bobby", LastName = "Flay", CreatedDate = new DateTime(2000, 1, 1), LastUpdatedDate = DateTime.Today });
                dbContext.Customers.Add(new Data.Entities.Customer() { Id = 4, FirstName = "Angelina", LastName = "Jolie", CreatedDate = new DateTime(2000, 1, 1), LastUpdatedDate = DateTime.Today });
                dbContext.Customers.Add(new Data.Entities.Customer() { Id = 5, FirstName = "Frank", LastName = "Sinatra", CreatedDate = new DateTime(2000, 1, 1), LastUpdatedDate = DateTime.Today });

            }

            dbContext.SaveChanges();
        }
    }
}
