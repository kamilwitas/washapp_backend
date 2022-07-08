using System.Net;
using Humanizer;

namespace washapp.services.identity.application.Exceptions;
public class InvalidAccessTokenException : AppException
{
    public override string Code { get; } = nameof(InvalidAccessTokenException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    public InvalidAccessTokenException() : base("Invalid access token")
    {
    }
}