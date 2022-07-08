using System.Net;
using Humanizer;

namespace washapp.services.identity.application.Exceptions;
public class InvalidAccessTokenOrRefreshTokenException : AppException
{
    public override string Code { get; } = nameof(InvalidAccessTokenOrRefreshTokenException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    public InvalidAccessTokenOrRefreshTokenException() : base("Invalid access token or refresh token")
    {
    }
}