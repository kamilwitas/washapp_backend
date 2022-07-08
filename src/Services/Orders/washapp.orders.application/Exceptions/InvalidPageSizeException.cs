using Humanizer;
using System.Net;

namespace washapp.orders.application.Exceptions
{
    public class InvalidPageSizeException : AppException
    {
        public override string Code { get; } = nameof(InvalidPageSizeException)
            .Underscore().Replace("_exception", String.Empty);

        public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

        public InvalidPageSizeException() : base("Invalid page size. Available page sizes: 5,10,50")
        {

        }
    }
}
