using washapp.orders.application.DTO;
using washapp.orders.application.Queries;
using washapp.orders.application.Services;
using washapp.orders.core.Repository;
using MediatR;

namespace washapp.orders.infrastructure.Queries;

public class GetCustomerOrdersHandler : IRequestHandler<GetCustomerOrders, IEnumerable<OrderDto>>
{
    private readonly IOrdersViewerService _ordersViewerService;

    public GetCustomerOrdersHandler(IOrdersViewerService ordersViewerService)
    {
        _ordersViewerService = ordersViewerService;
    }

    public async Task<IEnumerable<OrderDto>> Handle(GetCustomerOrders request, CancellationToken cancellationToken)
    {
        var orders = await _ordersViewerService.GetCustomerOrders(request.CustomerId);
        return orders;
    }
}