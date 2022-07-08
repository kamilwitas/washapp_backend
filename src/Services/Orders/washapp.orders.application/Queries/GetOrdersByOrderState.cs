using washapp.orders.core.Enums;
using MediatR;
using washapp.orders.application.DTO;
using washapp.orders.application.Models.Pagination;

namespace washapp.orders.application.Queries
{
    public class GetOrdersByOrderState : IRequest<PagedResult<OrderDto>>
    {
        public OrderState OrderState { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public GetOrdersByOrderState(OrderState orderState, int pageSize, int pageNumber)
        {
            OrderState = orderState;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}
