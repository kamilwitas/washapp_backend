using MediatR;
using washapp.orders.application.DTO;

namespace washapp.orders.application.Commands;

public class CreateOrder : IRequest
{
    public Guid CustomerId { get; set; }
    public IEnumerable<OrderLineDto> OrderLines { get; set; }

    public CreateOrder(Guid customerId, IEnumerable<OrderLineDto> orderLines)
    {
        CustomerId = customerId;
        OrderLines = orderLines;
    }
}