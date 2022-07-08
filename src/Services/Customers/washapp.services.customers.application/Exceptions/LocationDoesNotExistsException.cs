using System.Net;
using Humanizer;

namespace washapp.services.customers.application.Exceptions;

public class LocationDoesNotExistsException : AppException
{
    public override string Code { get; } = nameof(LocationDoesNotExistsException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;

    public LocationDoesNotExistsException(Guid locationId) : base($"Location with id: {locationId} does not exists")
    {
    }
}