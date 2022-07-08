using washapp.services.customers.domain.Entities;
using washapp.services.customers.domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using washapp.services.customers.application.Commands.AssortmentCategories;
using washapp.services.customers.application.Exceptions;

namespace washapp.services.customers.application.Commands.Handlers.AssortmentCategories;

public class CreateCategoryHandler : IRequestHandler<CreateCategory>
{
    private readonly IAssortmentCategoriesRepository _categoriesRepository;
    private readonly ILogger<CreateCategoryHandler> _logger;

    public CreateCategoryHandler(IAssortmentCategoriesRepository categoriesRepository, ILogger<CreateCategoryHandler> logger)
    {
        _categoriesRepository = categoriesRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(CreateCategory request, CancellationToken cancellationToken)
    {
        var category = await _categoriesRepository.GetByName(request.CategoryName);

        if (category is not null)
        {
            throw new CategoryAlreadyExistsException(request.CategoryName);
        }

        var newCategory = AssortmentCategory.Create(request.CategoryName);
        await _categoriesRepository.AddAsync(newCategory);
        _logger.LogInformation($"Category with name: {newCategory.CategoryName} has been created");
        
        return Unit.Value;
        
    }
}