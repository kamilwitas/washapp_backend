using System.Net;
using Humanizer;

namespace washapp.services.identity.application.Exceptions;

public class UserDoesNotExistsException : AppException
{
    public override string Code { get; } = nameof(UserDoesNotExistsException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;

    public UserDoesNotExistsException(Guid userId) : base($"User with id: {userId} does not exists")
    {
    }
    
    public UserDoesNotExistsException() : base($"User not exists")
    {
    }
}