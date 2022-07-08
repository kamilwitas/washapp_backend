using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using washapp.services.customers.application.Services;
using washapp.services.customers.domain.Repository;
using MassTransit;
using MediatR;
using washapp.services.customers.infrastructure.Database;
using washapp.services.customers.infrastructure.Exceptions.Middleware;
using washapp.services.customers.infrastructure.Repositories;
using washapp.services.customers.infrastructure.Services;

namespace washapp.services.customers.infrastructure
{
    public static class Extensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddDbContext<WashAppDbContext>(opt=>opt.UseSqlServer(configuration.GetConnectionString("WashAppDb")));
            services.AddTransient<ICustomersRepository, CustomersRepository>();
            services.AddTransient<IAssortmentsRepository, AssortmentsRepository>();
            services.AddTransient<IAssortmentCategoriesRepository, AssortmentCategoriesRepository>();
            services.AddTransient<ILocationsRepository, LocationsRepository>();
            services.AddTransient<ICustomerOperationsValidator, CustomerOperationsValidator>();
            services.AddScoped<ErrorHandlingMiddleware>();
            
            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["RabbitMq:hostname"],configuration["RabbitMq:virtualAddress"], h =>
                    {
                        h.Username(configuration["RabbitMq:username"]);
                        h.Password(configuration["RabbitMq:password"]);
                    });
                });
            });
            services.AddOptions<MassTransitHostOptions>()
                .Configure(options =>
            {
                options.WaitUntilStarted = true;
                options.StartTimeout = TimeSpan.FromSeconds(10);
                options.StopTimeout = TimeSpan.FromSeconds(30);
            });
            
        }

        public static void UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
        
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using (var dbContext = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<WashAppDbContext>())
            {
                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    dbContext.Database.Migrate();
                }
            }
        }
    }
}
