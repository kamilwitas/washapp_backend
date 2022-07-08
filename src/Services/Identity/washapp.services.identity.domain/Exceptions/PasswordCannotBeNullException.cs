using System.Net;
using Humanizer;
using washapp.services.identity.domain.Exceptions.Abstract;

namespace washapp.services.identity.domain.Exceptions;

public class PasswordCannotBeNullException : DomainException
{
    public override string Code { get; } = nameof(PasswordCannotBeNullException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    public PasswordCannotBeNullException() : base("Password cannot be empty")
    {
    }
}