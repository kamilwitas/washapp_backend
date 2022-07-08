using washapp.services.customers.application.Queries.AssortmentCategories;
using MediatR;
using washapp.services.customers.domain.Entities;
using washapp.services.customers.domain.Repository;

namespace washapp.services.customers.infrastructure.Queries.Handlers;

public class GetAssortmentCategoriesHandler : IRequestHandler<GetAssortmentCategories,IEnumerable<AssortmentCategory>>
{
    private readonly IAssortmentCategoriesRepository _categoriesRepository;

    public GetAssortmentCategoriesHandler(IAssortmentCategoriesRepository categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public async Task<IEnumerable<AssortmentCategory>> Handle(GetAssortmentCategories request, CancellationToken cancellationToken)
    {
        var categories = await _categoriesRepository.GetAllAsync();
        return categories;
        
    }
}