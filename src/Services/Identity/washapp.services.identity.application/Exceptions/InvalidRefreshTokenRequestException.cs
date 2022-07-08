using System.Net;
using Humanizer;

namespace washapp.services.identity.application.Exceptions
{
    public class InvalidRefreshTokenRequestException : AppException
    {
        public override string Code { get; } = nameof(InvalidRefreshTokenRequestException)
            .Underscore().Replace("_exception", string.Empty);

        public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

        public InvalidRefreshTokenRequestException() : base("Invalid refresh token request")
        {
        }
    }
}