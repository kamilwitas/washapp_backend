using System.Net;
using Humanizer;

namespace washapp.services.customers.application.Exceptions;

public class CategoryDoesNotExistsException : AppException
{
    public override string Code { get; } = nameof(CategoryDoesNotExistsException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;

    public CategoryDoesNotExistsException(Guid categoryId) : base($"Category with id: {categoryId} does not exists")
    {
    }
}