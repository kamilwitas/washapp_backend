using System.Net;
using Humanizer;
using washapp.services.customers.domain.Exceptions.Abstract;

namespace washapp.services.customers.domain.Exceptions;

public class AssortmentNameCannotBeEmptyException : DomainException
{
    public override string Code { get; } = nameof(AssortmentNameCannotBeEmptyException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    public AssortmentNameCannotBeEmptyException() : base("Assortment name cannot be empty")
    {
    }
}