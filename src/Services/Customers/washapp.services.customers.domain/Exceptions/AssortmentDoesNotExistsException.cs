using System.Net;
using Humanizer;
using washapp.services.customers.domain.Exceptions.Abstract;

namespace washapp.services.customers.domain.Exceptions;

public class AssortmentDoesNotExistsException : DomainException
{
    public override string Code { get; } = nameof(AssortmentDoesNotExistsException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    public AssortmentDoesNotExistsException(Guid assortmentId) : base($"Assortment with id: {assortmentId} does not exists")
    {
    }
}