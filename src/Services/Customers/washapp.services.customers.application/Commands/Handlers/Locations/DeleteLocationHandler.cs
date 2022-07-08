using washapp.services.customers.domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using washapp.services.customers.application.Commands.Locations;
using washapp.services.customers.application.Exceptions;

namespace washapp.services.customers.application.Commands.Handlers.Locations;

public class DeleteLocationHandler : IRequestHandler<DeleteLocation>
{
    private readonly ILocationsRepository _locationRepository;
    private readonly ICustomersRepository _customersRepository;
    private readonly ILogger<DelegatingHandler> _logger;

    public DeleteLocationHandler(ILocationsRepository locationRepository, ILogger<DelegatingHandler> logger,
        ICustomersRepository customersRepository)
    {
        _locationRepository = locationRepository;
        _logger = logger;
        _customersRepository = customersRepository;
    }

    public async Task<Unit> Handle(DeleteLocation request, CancellationToken cancellationToken)
    {
        var location = await _locationRepository.GetByIdAsync(request.LocationId);

        if (location is null)
        {
            throw new LocationDoesNotExistsException(request.LocationId);
        }

        var customers = await _customersRepository.GetByLocationAsync(location.Id);

        if (customers.Any())
        {
            throw new LocationIsAssignedToUserException(request.LocationId);
        }

        await _locationRepository.DeleteAsync(request.LocationId);
        _logger.LogWarning($"Location with id: {request.LocationId} has been deleted");
        return Unit.Value;
    }
    
    
}