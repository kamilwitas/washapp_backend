using System.Net;
using Humanizer;

namespace washapp.services.identity.application.Exceptions
{
    public class NotAuthorizedException : AppException
    {
        public override string Code { get; } = nameof(NotAuthorizedException)
            .Underscore().Replace("_exception", string.Empty);

        public override HttpStatusCode HttpStatusCode => HttpStatusCode.Unauthorized;

        public NotAuthorizedException() : base("Not authorized to perform password change")
        {

        }
    }
}
