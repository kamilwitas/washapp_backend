using washapp.services.customers.application.Services;
using washapp.services.customers.domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using washapp.services.customers.application.Commands.Assortments;
using washapp.services.customers.application.Exceptions;

namespace washapp.services.customers.application.Commands.Handlers.Assortments;

public class UpdateCustomerItemHandler : IRequestHandler<UpdateCustomerItem>
{
    private readonly ICustomersRepository _customersRepository;
    private readonly IAssortmentCategoriesRepository _categoriesRepository;
    private readonly ILogger<UpdateCustomerItemHandler> _logger;

    public UpdateCustomerItemHandler(ICustomersRepository customersRepository, IAssortmentCategoriesRepository categoriesRepository, 
        ILogger<UpdateCustomerItemHandler> logger)
    {
        _customersRepository = customersRepository;
        _categoriesRepository = categoriesRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateCustomerItem request, CancellationToken cancellationToken)
    {
        var assortmentCategory = await _categoriesRepository.GetByIdAsync(request.AssortmentCategoryId);

        if (assortmentCategory is null)
        {
            throw new AssortmentDoestNotExistsException(request.AssortmentCategoryId);
        }
        
        var customer = await _customersRepository.GetAsync(request.CustomerId);
        
        if (customer is null)
        {
            throw new CustomerDoesNotExistsException(request.CustomerId);
        }

        var duplicatedItem = customer.AssortmentItems
            .Where(x => x.AssortmentName.ToLower() == request.AssortmentName.ToLower() &&
                        x.AssortmentCategory.Id == request.AssortmentCategoryId &&
                        x.Id!=request.AssortmentId);

        if (duplicatedItem.Any())
        {
            throw new CustomerAssortmentDuplicateException();
        }

        var assortmentToUpdate = customer.AssortmentItems.Where(x => x.Id == request.AssortmentId).ToList();

        if (!assortmentToUpdate.Any())
        {
            throw new AssortmentDoestNotExistsException(request.AssortmentId);
        }
        
        customer.UpdateAssortment(request.AssortmentId, request.AssortmentName,request.Width,request.Height,request.Weight,
            assortmentCategory,request.MeasurementUnit,request.WeightUnit);

        await _customersRepository.UpdateAsync(customer);
        _logger.LogInformation($"Assortment item with id:{request.AssortmentId} has been modified");
         return Unit.Value;
    }
}