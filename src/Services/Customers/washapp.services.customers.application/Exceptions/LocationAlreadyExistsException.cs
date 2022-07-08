using System.Net;
using Humanizer;

namespace washapp.services.customers.application.Exceptions;

public class LocationAlreadyExistsException : AppException
{
    public override string Code { get; } = nameof(LocationAlreadyExistsException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    public LocationAlreadyExistsException() : base("Location already exists")
    {
    }
}