using System.Net;

namespace washapp.orders.core.Exceptions
{
    public class OrderLineNullException : DomainException
    {
        public override string Code { get; } = nameof(OrderLineNullException)
            .Replace("_exception",String.Empty);

        public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

        public OrderLineNullException():base("Order line cannot be null")
        {

        }
    }
}
