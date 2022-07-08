using washapp.orders.application.DTO;
using washapp.orders.application.Models.Pagination;
using washapp.orders.application.Queries;
using washapp.orders.application.Services;
using MediatR;

namespace washapp.orders.infrastructure.Queries
{
    public class GetOrdersByOrderStateHandler : IRequestHandler<GetOrdersByOrderState, PagedResult<OrderDto>>
    {
        private readonly IOrdersViewerService _ordersViewerService;

        public GetOrdersByOrderStateHandler(IOrdersViewerService ordersViewerService)
        {
            _ordersViewerService = ordersViewerService;
        }

        public async Task<PagedResult<OrderDto>> Handle(GetOrdersByOrderState request, CancellationToken cancellationToken)
        {
            var orders = await _ordersViewerService.GetPagedOrdersByOrderState(request);
            return orders;
        }
    }
}
