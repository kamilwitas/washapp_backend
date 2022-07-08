using washapp.orders.application.DTO;
using washapp.orders.application.Queries;
using washapp.orders.application.Services;
using MediatR;

namespace washapp.orders.infrastructure.Queries;

public class GetOrderDetailsHandler : IRequestHandler<GetOrderDetails, OrderDetailsDto>
{
    private readonly IOrdersViewerService _ordersViewerService;

    public GetOrderDetailsHandler(IOrdersViewerService ordersViewerService)
    {
        _ordersViewerService = ordersViewerService;
    }

    public async Task<OrderDetailsDto> Handle(GetOrderDetails request, CancellationToken cancellationToken)
    {
        var orderDetails = await _ordersViewerService.GetOrderDetails(request.OrderId);
        return orderDetails;
    }
}