using System.Net;
using Humanizer;

namespace washapp.services.customers.application.Exceptions;

public class LocationIsAssignedToUserException : AppException
{
    public override string Code { get; } = nameof(LocationIsAssignedToUserException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    public LocationIsAssignedToUserException(Guid locationId) : base($"Location with id: {locationId} has got assigned users")
    {
    }
}