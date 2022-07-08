using washapp.services.customers.domain.Entities;
using washapp.services.customers.domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using washapp.services.customers.application.Commands.Locations;
using washapp.services.customers.application.Exceptions;

namespace washapp.services.customers.application.Commands.Handlers.Locations;

public class UpdateLocationHandler : IRequestHandler<UpdateLocation>
{
    private readonly ILocationsRepository _locationsRepository;
    private readonly ILogger<UpdateLocationHandler> _logger;

    public UpdateLocationHandler(ILocationsRepository locationsRepository, ILogger<UpdateLocationHandler> logger)
    {
        _locationsRepository = locationsRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateLocation request, CancellationToken cancellationToken)
    {
        var location = await _locationsRepository.GetByIdAsync(request.LocationId);

        if (location is null)
        {
            throw new LocationDoesNotExistsException(request.LocationId);
        }

        Location updatedLocation = new Location(request.LocationName, request.LocationColor);

        if (await IsLocationDuplicated(request.LocationName,request.LocationColor))
        {
            throw new LocationAlreadyExistsException();
        }
        
        location.LocationColor = updatedLocation.LocationColor;
        location.LocationName = updatedLocation.LocationName;

        await _locationsRepository.UpdateAsync(location);
        _logger.LogInformation($"Location with id: {location.Id} has been updated");
        return Unit.Value;
    }
    
    private async Task<bool> IsLocationDuplicated(string locationName, string locationColor)
    {
        var locations = await _locationsRepository.GetByLocationName(locationName);
        
        if (locations is not null)
        {
            var duplicatedLocation = locations.FirstOrDefault(x => x.LocationColor == locationColor);

            if (duplicatedLocation is not null)
            {
                return true;
            }
        }

        return false;
    }
}