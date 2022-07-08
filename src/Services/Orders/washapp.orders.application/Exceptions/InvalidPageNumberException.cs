using Humanizer;
using System.Net;

namespace washapp.orders.application.Exceptions
{
    public class InvalidPageNumberException : AppException
    {
        public override string Code { get; } = nameof(InvalidPageNumberException)
            .Underscore().Replace("_exception",String.Empty);

        public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

        public InvalidPageNumberException() : base("Invalid page number. Page number must be greater or equal to page size")
        {

        }
    }
}
