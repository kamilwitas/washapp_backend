using MediatR;
using washapp.orders.application.DTO;

namespace washapp.orders.application.Queries;

public class GetCustomerOrders : IRequest<IEnumerable<OrderDto>>
{
    public Guid CustomerId { get; set; }
}