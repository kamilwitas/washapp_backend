using System.Net;
using Humanizer;

namespace washapp.orders.application.Exceptions;

public class OrderDoesNotExistsException : AppException
{
    public override string Code { get; } = nameof(OrderDoesNotExistsException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;

    public OrderDoesNotExistsException(Guid orderId) : base($"Order with id: {orderId} does not exists")
    {
    }
}