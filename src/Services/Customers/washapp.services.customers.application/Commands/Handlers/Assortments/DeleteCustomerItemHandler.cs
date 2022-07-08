using EventBus.Messages.IntegrationEvents.customers_service;
using washapp.services.customers.domain.Events;
using washapp.services.customers.domain.Repository;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using washapp.services.customers.application.Commands.Assortments;
using washapp.services.customers.application.Exceptions;

namespace washapp.services.customers.application.Commands.Handlers.Assortments;

public class DeleteCustomerItemHandler : IRequestHandler<DeleteCustomerItem>
{
    private readonly ICustomersRepository _customersRepository;
    private readonly IAssortmentsRepository _assortmentsRepository;
    private readonly ILogger<DeleteCustomerItemHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public DeleteCustomerItemHandler(ICustomersRepository customersRepository, IAssortmentsRepository assortmentsRepository, 
        ILogger<DeleteCustomerItemHandler> logger, IPublishEndpoint publishEndpoint)
    {
        _customersRepository = customersRepository;
        _assortmentsRepository = assortmentsRepository;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Unit> Handle(DeleteCustomerItem request, CancellationToken cancellationToken)
    {
        var customer = await _customersRepository.GetAsync(request.CustomerId);
        if (customer is null)
        {
            throw new CustomerDoesNotExistsException(request.CustomerId);
        }

        var itemToDelete = customer.AssortmentItems?.FirstOrDefault(x => x.Id == request.AssortmentId);

        if (itemToDelete is null)
        {
            throw new AssortmentDoestNotExistsException(request.AssortmentId);
        }
        
        customer.DeleteAssortmentItem(itemToDelete);
        await _customersRepository.UpdateAsync(customer);
        await _assortmentsRepository.DeleteAsync(itemToDelete.Id);
        _logger.LogInformation($"Assortment with id: {itemToDelete.Id} has been removed");

        var domainEvent = (UpdatedCustomer)customer.Events.FirstOrDefault();
        await _publishEndpoint.Publish<CustomerUpdated>(new CustomerUpdated(domainEvent.Customer.Id));
        
        return Unit.Value;
        
    }
}