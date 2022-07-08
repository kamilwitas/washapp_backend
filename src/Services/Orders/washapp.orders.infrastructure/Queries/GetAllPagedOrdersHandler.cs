using washapp.orders.application.DTO;
using washapp.orders.application.Models.Pagination;
using washapp.orders.application.Queries;
using washapp.orders.application.Services;
using MediatR;

namespace washapp.orders.infrastructure.Queries
{
    public class GetAllPagedOrdersHandler : IRequestHandler<GetAllPagedOrders, PagedResult<OrderDto>>
    {
        private readonly IOrdersViewerService _ordersViewerService;

        public GetAllPagedOrdersHandler(IOrdersViewerService ordersViewerService)
        {
            _ordersViewerService = ordersViewerService;
        }

        public async Task<PagedResult<OrderDto>> Handle(GetAllPagedOrders request, CancellationToken cancellationToken)
        {
            var ordersPagedResult = await _ordersViewerService.GetAllPagedOrdersWithOptionalFilters(request);

            return ordersPagedResult;
        }
    }
}
