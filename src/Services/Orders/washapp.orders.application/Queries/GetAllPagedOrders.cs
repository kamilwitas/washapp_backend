using MediatR;
using washapp.orders.application.DTO;
using washapp.orders.application.Models.Pagination;

namespace washapp.orders.application.Queries
{
    public class GetAllPagedOrders : IRequest<PagedResult<OrderDto>>
    {
        public string SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; }

        public GetAllPagedOrders(string searchPhrase, int pageNumber, int pageSize, 
            string sortBy, SortDirection sortDirection)
        {
            SearchPhrase = searchPhrase;
            PageNumber = pageNumber;
            PageSize = pageSize;
            SortBy = sortBy;
            SortDirection = sortDirection;
        }
    }


}
