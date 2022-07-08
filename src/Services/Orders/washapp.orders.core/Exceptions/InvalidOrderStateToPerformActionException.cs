using Humanizer;
using System.Net;
using washapp.orders.core.Enums;

namespace washapp.orders.core.Exceptions
{
    class InvalidOrderStateToPerformActionException : DomainException
    {
        public override string Code { get; } = nameof(InvalidOrderStateToPerformActionException)
            .Underscore().Replace("_exception",String.Empty);

        public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;

        public InvalidOrderStateToPerformActionException(OrderState orderState):base($"Cannot perform actions in order with status: { orderState.ToString()}")
        {

        }
    }
}
