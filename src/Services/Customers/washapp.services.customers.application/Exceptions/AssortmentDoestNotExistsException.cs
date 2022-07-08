using System.Net;
using Humanizer;

namespace washapp.services.customers.application.Exceptions;

public class AssortmentDoestNotExistsException : AppException
{
    public override string Code { get; } = nameof(AssortmentDoestNotExistsException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;

    public AssortmentDoestNotExistsException(Guid assortmentId) : base($"Assortment with id: {assortmentId} does not exists")
    {
    }
}