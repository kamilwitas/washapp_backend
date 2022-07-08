using MediatR;
using washapp.orders.application.DTO;

namespace washapp.orders.application.Commands
{
    public class CreateOrderExecutions : IRequest
    {
        public Guid OrderId { get; set; }
        public IEnumerable<OrderExecutionRequestDto> OrderExecutionRequests { get; set; }

        public CreateOrderExecutions(Guid orderId, IEnumerable<OrderExecutionRequestDto> orderExecutionRequests)
        {
            OrderId = orderId;
            OrderExecutionRequests = orderExecutionRequests;
        }
    }
}
