using System.Net;
using Humanizer;
using washapp.services.customers.domain.Exceptions.Abstract;

namespace washapp.services.customers.domain.Exceptions;

public class InvalidCustomersDetailsException : DomainException
{
    public override string Code { get; } = nameof(InvalidCustomersDetailsException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    public InvalidCustomersDetailsException() : base("Invalid customer details")
    {
    }
}