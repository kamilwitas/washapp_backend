using EventBus.Messages.IntegrationEvents.customers_service;
using washapp.services.customers.domain.Events;
using washapp.services.customers.domain.Repository;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using washapp.services.customers.application.Commands.Customers;
using washapp.services.customers.application.Exceptions;
using washapp.services.customers.application.Services;

namespace washapp.services.customers.application.Commands.Handlers.Customers;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomer>
{
    private readonly ICustomersRepository _customersRepository;
    private readonly ICustomerOperationsValidator _customerOperationsValidator;
    private readonly ILogger<UpdateCustomerHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public UpdateCustomerHandler(ICustomersRepository customersRepository, ICustomerOperationsValidator customerOperationsValidator, 
        ILogger<UpdateCustomerHandler> logger, IPublishEndpoint publishEndpoint)
    {
        _customersRepository = customersRepository;
        _customerOperationsValidator = customerOperationsValidator;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Unit> Handle(UpdateCustomer request, CancellationToken cancellationToken)
    {
        var customerToUpdate = await _customersRepository.GetAsync(request.CustomerId);
        
        if (customerToUpdate is null)
        {
            throw new CustomerDoesNotExistsException(request.CustomerId);
        }

        await _customerOperationsValidator.CheckForCustomerDuplicates(request.FirstName, request.LastName,
            request.CompanyName, request.CustomerColor, true, customerToUpdate.Id);

        var location = await _customerOperationsValidator.CheckIfLocationExists(request.LocationId);
        
        customerToUpdate.Update(request.FirstName,request.LastName,request.CompanyName,
            request.CustomerColor,request.Street,request.LocalNumber,request.PostCode, location);
        
        await _customersRepository.UpdateAsync(customerToUpdate);

        var domainEvent = (UpdatedCustomer)customerToUpdate.Events.FirstOrDefault();
        await _publishEndpoint.Publish<CustomerUpdated>(new CustomerUpdated(domainEvent.Customer.Id));
        
        return Unit.Value;
    }
}