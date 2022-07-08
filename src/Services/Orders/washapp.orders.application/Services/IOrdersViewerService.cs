using washapp.orders.application.DTO;
using washapp.orders.application.Models.Pagination;
using washapp.orders.application.Queries;

namespace washapp.orders.application.Services
{
    public interface IOrdersViewerService
    {
        Task<PagedResult<OrderDto>>GetAllPagedOrdersWithOptionalFilters(GetAllPagedOrders query);
        Task<PagedResult<OrderDto>>GetPagedOrdersByOrderState(GetOrdersByOrderState query);
        Task<IEnumerable<OrderDto>>GetCustomerOrders(Guid customerId);
        Task<OrderDetailsDto> GetOrderDetails(Guid orderId);

    }
}
