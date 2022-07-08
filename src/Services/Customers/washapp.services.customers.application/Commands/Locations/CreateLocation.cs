using MediatR;

namespace washapp.services.customers.application.Commands.Locations;

public class CreateLocation : IRequest
{
    public string LocationName { get; set; }
    public string LocationColor { get; set; }

    public CreateLocation(string locationName, string locationColor)
    {
        LocationName = locationName;
        LocationColor = locationColor;
    }
}