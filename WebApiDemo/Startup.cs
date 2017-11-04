using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApiDemo.Data;
using AutoMapper;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.EntityFrameworkCore;
using WebApiDemo.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;

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
            var jwtConfigOptions = Configuration.GetSection("Jwt").Get<JwtConfigurationOptions>();
            services.Configure<JwtConfigurationOptions>(Configuration.GetSection("Jwt"));

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigOptions.SecretKey)),

                    ValidateIssuer = true,
                    ValidIssuer = jwtConfigOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtConfigOptions.Audience,

                    ValidateLifetime = true, //validate the expiration and not before values in the token

                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
            });

            services.AddCors();

            services.AddDbContext<DemoDbContext>(opt => opt.UseInMemoryDatabase("demoDb"));

            services.AddMvc();

            services.AddAutoMapper();

            // see: https://elanderson.net/2017/10/swagger-and-swashbuckle-with-asp-net-core-2/
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "WebApiDemo API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
            });

            //register types for DI
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<AppConfig, AppConfig>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseCors(policyBuilder =>
                policyBuilder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
            );

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiDemo V1");
            });

            if (env.IsDevelopment())
            {
                // seed database with data
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var dbContext = serviceScope.ServiceProvider.GetService<DemoDbContext>();
                    DatabaseInitializer.AddDatabaseSeedData(dbContext);
                }
            }
            
        }
    }
}
