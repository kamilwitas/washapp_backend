using System.Net;
using Humanizer;

namespace washapp.services.customers.application.Exceptions;

public class CustomerIsAlreadyExistsException : AppException
{
    public override string Code { get; } = nameof(CustomerIsAlreadyExistsException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    public CustomerIsAlreadyExistsException() : base("Customer is already exists")
    {
    }
}