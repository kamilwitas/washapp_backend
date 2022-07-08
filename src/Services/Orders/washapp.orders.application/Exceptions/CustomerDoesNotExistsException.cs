using System.Net;
using Humanizer;

namespace washapp.orders.application.Exceptions;

public class CustomerDoesNotExistsException : AppException
{
    public override string Code { get; } = nameof(CustomerDoesNotExistsException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;

    public CustomerDoesNotExistsException(Guid customerId) : base($"Customer with id: {customerId} does not exists")
    {
    }
}