using Humanizer;
using System.Net;

namespace washapp.orders.core.Exceptions
{
    public class InvalidOrderLineQuantityException : DomainException
    {
        public override string Code { get; } = nameof(InvalidOrderLineQuantityException)
            .Underscore().Replace("_exception",string.Empty);

        public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

        public InvalidOrderLineQuantityException() :base("Quantity of item must be greater than zero")
        {

        }
    }
}
