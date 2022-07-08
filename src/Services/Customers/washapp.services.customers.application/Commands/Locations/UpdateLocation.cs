using MediatR;

namespace washapp.services.customers.application.Commands.Locations;

public class UpdateLocation : IRequest
{
    public Guid LocationId { get; set; }
    public string LocationName { get; set; }
    public string LocationColor { get; set; }

    public UpdateLocation(Guid locationId, string locationName, string locationColor)
    {
        LocationId = locationId;
        LocationName = locationName;
        LocationColor = locationColor;
    }
}