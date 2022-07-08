using System.Net;
using Humanizer;

namespace washapp.services.identity.application.Exceptions
{
    public class LoginErrorException : AppException
    {
        public override string Code { get; } = nameof(LoginErrorException)
            .Underscore().Replace("_exception", string.Empty);

        public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

        public LoginErrorException(string message) : base("")
        {

        }
    }
}
