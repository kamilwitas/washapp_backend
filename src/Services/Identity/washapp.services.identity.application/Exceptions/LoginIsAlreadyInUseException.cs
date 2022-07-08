using System.Net;
using Humanizer;

namespace washapp.services.identity.application.Exceptions;

public class LoginIsAlreadyInUseException : AppException
{
    public override string Code { get; } = nameof(LoginIsAlreadyInUseException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    public LoginIsAlreadyInUseException(string login) : base($"Login: {login} is already in use")
    {
    }
}