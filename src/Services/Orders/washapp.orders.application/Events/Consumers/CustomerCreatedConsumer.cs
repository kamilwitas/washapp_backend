using washapp.orders.application.DTO.Contracts;
using washapp.orders.core.Entities;
using washapp.orders.core.Repository;
using EventBus.Messages.IntegrationEvents.customers_service;
using MassTransit;
using Microsoft.Extensions.Logging;
using washapp.orders.application.Services;

namespace washapp.orders.application.Events.Consumers;

public class CustomerCreatedConsumer : IConsumer<CustomerCreated>
{
    private readonly ICustomersClient _customersClient;
    private readonly ICustomersMongoRepository _customersRepository;
    private readonly ILogger<CustomerCreatedConsumer> _logger;
    
    public CustomerCreatedConsumer(ICustomersClient customersClient,
        ICustomersMongoRepository customersRepository,ILogger<CustomerCreatedConsumer> logger)
    {
        _customersClient = customersClient;
        _customersRepository= customersRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CustomerCreated> context)
    {
        
        var customer = await _customersRepository.GetCustomerAsync(context.Message.CustomerId);
        
        if (customer is not null)
        {
            return;
        }

        var newCustomerDto = await _customersClient.GetCustomer(context.Message.CustomerId);

        var newCustomer = newCustomerDto.AsBusiness();

        await _customersRepository.AddAsync(newCustomer);
        
        _logger.LogInformation($"New customer created in orders_service: {newCustomer.Id}");
        
        return;
        
        
    }
}