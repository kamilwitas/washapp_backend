using System.Net;
using Humanizer;

namespace washapp.services.customers.application.Exceptions;

public class ItemsWithThisCategoryExistsException : AppException
{
    public override string Code { get; } = nameof(ItemsWithThisCategoryExistsException)
        .Underscore().Replace("_exception", string.Empty);

    public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

    public ItemsWithThisCategoryExistsException() : base("There are items exist which have this category assigned")
    {
    }
}