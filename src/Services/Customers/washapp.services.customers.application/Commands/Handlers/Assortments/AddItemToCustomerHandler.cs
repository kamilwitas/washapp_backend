using EventBus.Messages.IntegrationEvents.customers_service;
using washapp.services.customers.domain.Entities;
using washapp.services.customers.domain.Enums;
using washapp.services.customers.domain.Events;
using washapp.services.customers.domain.Repository;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using washapp.services.customers.application.Commands.Assortments;
using washapp.services.customers.application.Exceptions;
using washapp.services.customers.application.Services;

namespace washapp.services.customers.application.Commands.Handlers.Assortments;

public class AddItemToCustomerHandler : IRequestHandler<AddItemToCustomer>
{
    private readonly ICustomersRepository _customersRepository;
    private readonly ICustomerOperationsValidator _customerOperationsValidator;
    private readonly IAssortmentCategoriesRepository _categoriesRepository;
    private readonly ILogger<AddItemToCustomerHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public AddItemToCustomerHandler(ICustomersRepository customersRepository, ICustomerOperationsValidator customerOperationsValidator, 
        IAssortmentCategoriesRepository categoriesRepository,
        ILogger<AddItemToCustomerHandler> logger, IPublishEndpoint publishEndpoint)
    {
        _customersRepository = customersRepository;
        _customerOperationsValidator = customerOperationsValidator;
        _categoriesRepository = categoriesRepository;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Unit> Handle(AddItemToCustomer request, CancellationToken cancellationToken)
    {
        var customer = await _customersRepository.GetAsync(request.CustomerId);
        if (customer is null)
        {
            throw new CustomerDoesNotExistsException(request.CustomerId);
        }

        var assortmentCategory = await _categoriesRepository.GetByIdAsync(request.AssortmentCategoryId);

        if (assortmentCategory is null)
        {
            throw new CategoryDoesNotExistsException(request.AssortmentCategoryId);
        }

        var existAssortment = customer.AssortmentItems
            .FirstOrDefault(x => x.AssortmentName == request.AssortmentName &&
                                 x.AssortmentCategory.Id == assortmentCategory.Id);

        if (existAssortment is not null)
        {
            throw new CustomerDuplicateItemException();
        }

        var newAssortment = Assortment.Create(request.AssortmentName, request.Width, request.Height, request.Weight,
            assortmentCategory, request.MeasurementUnit = MeasurementUnit.Cm, request.WeightUnit = WeightUnit.Kg);
        
        customer.AddAssortment(newAssortment);

        await _customersRepository.UpdateAsync(customer);

        var domainEvent = (UpdatedCustomer)customer.Events.FirstOrDefault();
        await _publishEndpoint.Publish<CustomerUpdated>(new CustomerUpdated(domainEvent.Customer.Id));
        
        
        
        return Unit.Value;

    }
}