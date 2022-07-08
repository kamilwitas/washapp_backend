using System.Net;
using Humanizer;

namespace washapp.services.customers.application.Exceptions;

public class CategoryAlreadyExistsException : AppException
{
    public override string Code { get; } = nameof(CategoryAlreadyExistsException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    public CategoryAlreadyExistsException(string categoryName) : base($"Category with name: {categoryName} already exists")
    {
    }
}