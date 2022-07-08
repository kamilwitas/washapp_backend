using washapp.services.customers.domain.Entities;
using washapp.services.customers.domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using washapp.services.customers.application.Commands.AssortmentCategories;
using washapp.services.customers.application.Exceptions;

namespace washapp.services.customers.application.Commands.Handlers.AssortmentCategories;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategory>
{
    private readonly IAssortmentCategoriesRepository _categoriesRepository;
    private readonly ILogger<UpdateCategoryHandler> _logger;

    public UpdateCategoryHandler(IAssortmentCategoriesRepository categoriesRepository, ILogger<UpdateCategoryHandler> logger)
    {
        _categoriesRepository = categoriesRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateCategory request, CancellationToken cancellationToken)
    {
        var categoryToUpdate = await _categoriesRepository.GetByIdAsync(request.CategoryId);
        await ValidIfCategoryCanBeUpdated(request,categoryToUpdate);
        
        categoryToUpdate.Update(request.CategoryName);
        await _categoriesRepository.UpdateAsync(categoryToUpdate);
        _logger.LogInformation($"Category with id: {categoryToUpdate.Id} has been updated");
        
        return Unit.Value;
    }

    private async Task ValidIfCategoryCanBeUpdated(UpdateCategory request, AssortmentCategory categoryToUpdate)
    {
        if (categoryToUpdate is null)
        {
            throw new CategoryDoesNotExistsException(request.CategoryId);
        }

        var duplicatedCategory = await _categoriesRepository.GetByName(request.CategoryName);
        
        if (duplicatedCategory is not null)
        {
            throw new CategoryAlreadyExistsException(duplicatedCategory.CategoryName);
        }

    }
}