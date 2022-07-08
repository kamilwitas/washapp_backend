using washapp.services.customers.domain.Entities;
using MediatR;

namespace washapp.services.customers.application.Queries.AssortmentCategories;

public class GetAssortmentCategories : IRequest<IEnumerable<AssortmentCategory>>
{
    
}