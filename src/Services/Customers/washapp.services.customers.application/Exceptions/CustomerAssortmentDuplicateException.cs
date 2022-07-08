using System.Net;
using Humanizer;

namespace washapp.services.customers.application.Exceptions;

public class CustomerAssortmentDuplicateException : AppException
{
    public override string Code { get; } = nameof(CustomerAssortmentDuplicateException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    public CustomerAssortmentDuplicateException() : base("Customer already has the same item")
    {
    }
}