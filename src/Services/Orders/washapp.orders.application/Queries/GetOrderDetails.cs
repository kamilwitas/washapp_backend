using MediatR;
using washapp.orders.application.DTO;

namespace washapp.orders.application.Queries;

public class GetOrderDetails : IRequest<OrderDetailsDto>
{
    public Guid OrderId { get; set; }
}