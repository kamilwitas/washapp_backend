using System.Net;
using Humanizer;

namespace washapp.orders.application.Exceptions;

public class AssortmentDoesNotExistsException : AppException
{
    public override string Code { get; } = nameof(AssortmentDoesNotExistsException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
    
    public AssortmentDoesNotExistsException(Guid assortmentId) : base($"Assortment with id: {assortmentId} does not exists")
    {
    }
}