using System.Net;
using System.Runtime.CompilerServices;
using Humanizer;
using washapp.services.customers.domain.Exceptions.Abstract;

namespace washapp.services.customers.domain.Exceptions;

public class CategoryCannotBeNullException : DomainException
{
    public override string Code { get; } = nameof(CategoryCannotBeNullException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    public CategoryCannotBeNullException() : base("Category cannot be null")
    {
    }
}