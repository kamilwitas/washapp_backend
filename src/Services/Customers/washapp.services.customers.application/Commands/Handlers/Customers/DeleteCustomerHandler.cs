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

public class DeleteCustomerHandler : IRequestHandler<DeleteCustomer>
{
    private readonly ICustomersRepository _customersRepository;
    private readonly ICustomerOperationsValidator _customerOperationsValidator;
    private readonly IAssortmentsRepository _assortmentsRepository;
    private readonly ILogger<DeleteCustomerHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public DeleteCustomerHandler(ICustomersRepository customersRepository, ICustomerOperationsValidator customerOperationsValidator,
        IAssortmentsRepository assortmentsRepository,
        ILogger<DeleteCustomerHandler> logger,IPublishEndpoint publishEndpoint)
    {
        _customersRepository = customersRepository;
        _customerOperationsValidator = customerOperationsValidator;
        _assortmentsRepository = assortmentsRepository;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Unit> Handle(DeleteCustomer request, CancellationToken cancellationToken)
    {
        var customer = await _customersRepository.GetAsync(request.CustomerId);
        if (customer is null)
        {
            throw new CustomerDoesNotExistsException(request.CustomerId);
        }

        customer.DeleteCustomer();
        await _customersRepository.DeleteAsync(customer.Id);

        var domainEvent = (DeletedCustomer)customer.Events.FirstOrDefault();

        await _publishEndpoint.Publish<CustomerDeleted>(new CustomerDeleted(domainEvent.Customer.Id));
        return Unit.Value;
    }
}