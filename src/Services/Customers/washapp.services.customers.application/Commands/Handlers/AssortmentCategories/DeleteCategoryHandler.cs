using System.Reflection.Metadata.Ecma335;
using washapp.services.customers.domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using washapp.services.customers.application.Commands.AssortmentCategories;
using washapp.services.customers.application.Exceptions;

namespace washapp.services.customers.application.Commands.Handlers.AssortmentCategories;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategory>
{
    private readonly IAssortmentCategoriesRepository _categoriesRepository;
    private readonly IAssortmentsRepository _assortmentsRepository;
    private readonly ILogger<DeleteCategoryHandler> _logger;

    public DeleteCategoryHandler(IAssortmentCategoriesRepository categoriesRepository, IAssortmentsRepository assortmentsRepository, ILogger<DeleteCategoryHandler> logger)
    {
        _categoriesRepository = categoriesRepository;
        _assortmentsRepository = assortmentsRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteCategory request, CancellationToken cancellationToken)
    {
        var category = await _categoriesRepository.GetByIdAsync(request.CategoryId);

        if (category is null)
        {
            throw new CategoryDoesNotExistsException(request.CategoryId);
        }

        var assortmentsWithDeletedCategory = await _assortmentsRepository
            .GetAssortmentByExpression(x => x.AssortmentCategory.Id == category.Id);

        if (assortmentsWithDeletedCategory.Any())
        {
            throw new ItemsWithThisCategoryExistsException();
        }

        await _categoriesRepository.DeleteAsync(request.CategoryId);
        _logger.LogInformation($"Category with name: {category.CategoryName} has been removed");

        return Unit.Value;
    }
}