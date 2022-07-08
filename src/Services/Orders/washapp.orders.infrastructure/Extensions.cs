using System.Reflection;
using washapp.orders.application.Events.Consumers;
using washapp.orders.application.Services;
using washapp.orders.core.Repository;
using Convey;
using Convey.Persistence.MongoDB;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using washapp.orders.infrastructure.Exceptions.Middleware;
using washapp.orders.infrastructure.Mongo.Documents;
using washapp.orders.infrastructure.Mongo.Repositories;
using washapp.orders.infrastructure.Services;

namespace washapp.orders.infrastructure;

public static class Extensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration )
    {
        services.AddTransient<ICustomersClient, CustomersClient>(x=>new CustomersClient(configuration.GetSection("CustomersGrpc").Value));
        services.AddTransient<ErrorHandlerMiddleware>();
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient<IOrdersViewerService, OrderViewerService>();
        services.AddTransient<ICustomersMongoRepository, CustomersMongoRepository>();
        services.AddTransient<IOrdersRepository,OrdersRepository>();
        services.AddTransient<IUserPrincipalService, UserPrincipalService>();   
        services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();

        services.AddConvey()
            .AddMongo()
            .AddMongoRepository<CustomerDocument, Guid>("customers")
            .AddMongoRepository<OrderDocument, Guid>("orders");
            
        
        services.AddMassTransit(config =>
        {
            config.AddConsumer<CustomerCreatedConsumer>()
                .Endpoint(x =>
                {
                    x.Name = configuration["RabbitMq:queue"];
                });
            config.AddConsumer<CustomerUpdatedConsumer>()
                .Endpoint(x =>
                {
                    x.Name = configuration["RabbitMq:queue"];
                });
            config.AddConsumer<CustomerDeletedConsumer>()
                .Endpoint(x =>
                {
                    x.Name = configuration["RabbitMq:queue"];
                });

            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(configuration["RabbitMq:hostname"],configuration["RabbitMq:virtualAddress"], h =>
                {
                    h.Username(configuration["RabbitMq:username"]);
                    h.Password(configuration["RabbitMq:password"]);
                });
                cfg.UseMessageRetry(r=>r.Immediate(5));
                cfg.ConfigureEndpoints(ctx);
            });
        });
        services.AddOptions<MassTransitHostOptions>()
            .Configure(options =>
            {
                options.WaitUntilStarted = true;
                options.StartTimeout = TimeSpan.FromSeconds(10);
                options.StopTimeout = TimeSpan.FromSeconds(30);
            });

        services.AddGrpc();
    }

    public static void UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseConvey();
    }
}