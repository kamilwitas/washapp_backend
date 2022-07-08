using System.Net;
using Humanizer;
using washapp.services.customers.domain.Exceptions.Abstract;

namespace washapp.services.customers.domain.Exceptions;

public class InvalidLocationNameOrColorException : DomainException
{
    public override string Code { get; } = nameof(InvalidLocationNameOrColorException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    public InvalidLocationNameOrColorException() : base("Invalid location name or location color")
    {
    }
}