using washapp.orders.application.DTO.Contracts;
using washapp.orders.core.Repository;
using EventBus.Messages.IntegrationEvents.customers_service;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using washapp.orders.application.Exceptions;
using washapp.orders.application.Services;

namespace washapp.orders.application.Events.Consumers;

public class CustomerUpdatedConsumer : IConsumer<CustomerUpdated>
{
    private readonly ICustomersMongoRepository _customersRepository;
    private readonly ICustomersClient _customersClient;
    private readonly ILogger<CustomerCreatedConsumer> _logger;

    public CustomerUpdatedConsumer(ICustomersMongoRepository customersRepository, ICustomersClient customersClient, 
        ILogger<CustomerCreatedConsumer> logger)
    {
        _customersRepository = customersRepository;
        _customersClient = customersClient;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CustomerUpdated> context)
    {
        var customer = await _customersRepository.GetCustomerAsync(context.Message.CustomerId);

        var updatedCustomerDto = await _customersClient.GetCustomer(context.Message.CustomerId);

        if (updatedCustomerDto is null)
        {
            throw new CustomerDoesNotExistsException(context.Message.CustomerId);
        }

        var updatedCustomer = updatedCustomerDto.AsBusiness();
        
        if (customer is null)
        {
            await _customersRepository.AddAsync(updatedCustomer);
            _logger.LogInformation($"Customer with id: {updatedCustomer.Id} has been created");
            return;
        }
        customer.Update(updatedCustomer.CompanyName,updatedCustomer.CustomerColor,updatedCustomer.LocationId,updatedCustomer.Assortments);
        await _customersRepository.UpdateAsync(customer);
        _logger.LogInformation($"Customer with id: {customer.Id} has been updated");
        
    }
}