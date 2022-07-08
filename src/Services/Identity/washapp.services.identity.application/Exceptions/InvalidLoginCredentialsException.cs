using System.Net;
using Humanizer;

namespace washapp.services.identity.application.Exceptions;
public class InvalidLoginCredentialsException : AppException
{
    public override string Code { get; } = nameof(InvalidLoginCredentialsException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    public InvalidLoginCredentialsException() : base("Invalid login credentials")
    {
    }
}