using washapp.services.customers.domain.Entities;
using washapp.services.customers.domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using washapp.services.customers.application.Commands.Locations;
using washapp.services.customers.application.Exceptions;

namespace washapp.services.customers.application.Commands.Handlers.Locations;

public class CreateLocationHandler : IRequestHandler<CreateLocation>
{
    private readonly ILocationsRepository _locationsRepository;
    private readonly ILogger<CreateLocationHandler> _logger;

    public CreateLocationHandler(ILocationsRepository locationsRepository, ILogger<CreateLocationHandler> logger)
    {
        _locationsRepository = locationsRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(CreateLocation request, CancellationToken cancellationToken)
    {
        if (await IsLocationDuplicated(request.LocationName,request.LocationColor))
        {
            throw new LocationAlreadyExistsException();
        }
        var newLocation = Location.Create(request.LocationName, request.LocationColor);
        await _locationsRepository.AddAsync(newLocation);
        _logger.LogInformation($"Location: {newLocation.LocationName} with color: {newLocation.LocationColor} has been created");
        
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