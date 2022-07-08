using System.Net;
using Humanizer;

namespace washapp.services.identity.application.Exceptions;

public class RoleDoesNotExistsException : AppException
{
    public override string Code { get; } = nameof(RoleDoesNotExistsException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    public RoleDoesNotExistsException() : base("Role does not exists")
    {
    }
}