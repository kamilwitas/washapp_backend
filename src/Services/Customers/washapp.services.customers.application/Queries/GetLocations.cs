using washapp.services.customers.domain.Entities;
using MediatR;

namespace washapp.services.customers.application.Queries;

public class GetLocations : IRequest<IEnumerable<Location>>
{
    
}