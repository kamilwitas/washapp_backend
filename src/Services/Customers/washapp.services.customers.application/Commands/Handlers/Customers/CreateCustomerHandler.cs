using EventBus.Messages.IntegrationEvents.customers_service;
using washapp.services.customers.domain.Entities;
using washapp.services.customers.domain.Events;
using washapp.services.customers.domain.Repository;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using washapp.services.customers.application.Commands.Customers;
using washapp.services.customers.application.Services;

namespace washapp.services.customers.application.Commands.Handlers.Customers;

public class CreateCustomerHandler : IRequestHandler<CreateCustomer>
{
    private readonly ICustomersRepository _customersRepository;
    private readonly ICustomerOperationsValidator _customerOperationsValidator;
    private readonly ILogger<CreateCustomerHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateCustomerHandler(ICustomersRepository customersRepository, ILogger<CreateCustomerHandler> logger,
        ICustomerOperationsValidator customerOperationsValidator,IPublishEndpoint publishEndpoint)
    {
        _customersRepository = customersRepository;
        _logger = logger;
        _customerOperationsValidator = customerOperationsValidator;
        _publishEndpoint = publishEndpoint;

    }

    public async Task<Unit> Handle(CreateCustomer request, CancellationToken cancellationToken)
    {
        await _customerOperationsValidator.CheckForCustomerDuplicates(request.FirstName, request.LastName,
            request.CompanyName, request.CustomerColor);

        var location = await _customerOperationsValidator.CheckIfLocationExists(request.LocationId);

        var newCustomer = CreateCustomerFromRequest(request,location);

        await _customersRepository.AddAsync(newCustomer);
        _logger.LogInformation($"Customer with: {newCustomer.Id} has been created");

        CreatedCustomer createdCustomerEvent = (CreatedCustomer)newCustomer.Events.FirstOrDefault();

        await _publishEndpoint.Publish<CustomerCreated>(new CustomerCreated(createdCustomerEvent.Customer.Id));
        
        
        return Unit.Value;

    }

    private Customer CreateCustomerFromRequest(CreateCustomer request, Location location)
    {
        var customerAddress = Address.Create(request.Street, request.LocalNumber, request.PostCode, location);

        var newCustomer = Customer.Create(request.FirstName, request.LastName, customerAddress, request.CompanyName,
            request.CustomerColor, DateTime.UtcNow);
        return newCustomer;
    }
}