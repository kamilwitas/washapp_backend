using System.Net;
using Humanizer;
using washapp.services.customers.domain.Exceptions.Abstract;

namespace washapp.services.customers.domain.Exceptions;

public class LocationCannotBeNullException : DomainException
{
    public override string Code { get; } = nameof(LocationCannotBeNullException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    public LocationCannotBeNullException() : base("Location cannot be null")
    {
    }
}