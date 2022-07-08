using System.Net;
using Humanizer;

namespace washapp.services.customers.application.Exceptions;

public class CustomerDuplicateItemException : AppException
{
    public override string Code { get; } = nameof(CustomerDuplicateItemException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    public CustomerDuplicateItemException() : base("Customer already has this item")
    {
    }
}