using washapp.services.customers.application.Queries;
using washapp.services.customers.domain.Entities;
using washapp.services.customers.domain.Repository;
using MediatR;

namespace washapp.services.customers.infrastructure.Queries.Handlers;

public class GetLocationsHandler : IRequestHandler<GetLocations, IEnumerable<Location>>
{
    private readonly ILocationsRepository _locationsRepository;

    public GetLocationsHandler(ILocationsRepository locationsRepository)
    {
        _locationsRepository = locationsRepository;
    }

    public async Task<IEnumerable<Location>> Handle(GetLocations request, CancellationToken cancellationToken)
    {
        var locations = await _locationsRepository.GetAllAsync();
        return locations;
    }
}