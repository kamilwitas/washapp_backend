using System.Net;
using Humanizer;
using washapp.services.customers.domain.Exceptions.Abstract;

namespace washapp.services.customers.domain.Exceptions;

public class InvalidCategoryNameException : DomainException
{
    public override string Code { get; } = nameof(InvalidCategoryNameException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    public InvalidCategoryNameException() : base("Invalid category name")
    {
        
    }
}