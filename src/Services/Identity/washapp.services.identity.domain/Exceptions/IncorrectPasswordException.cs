using System.Net;
using Humanizer;
using washapp.services.identity.domain.Exceptions.Abstract;

namespace washapp.services.identity.domain.Exceptions;

public class IncorrectPasswordException : DomainException
{
    public override string Code { get; } = nameof(IncorrectPasswordException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    public IncorrectPasswordException() : base("Incorrect password")
    {
    }
}