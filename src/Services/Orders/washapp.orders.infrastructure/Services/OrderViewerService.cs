using washapp.orders.application.DTO;
using washapp.orders.application.Exceptions;
using washapp.orders.application.Queries;
using washapp.orders.application.Services;
using washapp.orders.infrastructure.Mongo.Mappings;
using Convey.Persistence.MongoDB;
using MongoDB.Driver;
using System.Linq.Expressions;
using washapp.orders.application.Models.Pagination;
using washapp.orders.infrastructure.Mongo.Documents;
using SortDirection = washapp.orders.application.Models.Pagination.SortDirection;

namespace washapp.orders.infrastructure.Services
{
    internal class OrderViewerService : IOrdersViewerService
    {
        private readonly IMongoRepository<OrderDocument,Guid> _ordersRepository;

        public OrderViewerService(IMongoRepository<OrderDocument,Guid>ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<PagedResult<OrderDto>> GetPagedOrdersByOrderState(GetOrdersByOrderState query)
        {
            PageSizeAndNumberValidate(query.PageNumber, query.PageSize);

            var baseQuery = _ordersRepository.Collection
                .AsQueryable()
                .Where(x => x.OrderState == query.OrderState);
                

            var orderDocuments = baseQuery.Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            var totalItemsCount = baseQuery.Count();

            var orders = orderDocuments.Select(x => x.AsDto());

            var result = new PagedResult<OrderDto>(orders.ToList(), totalItemsCount, query.PageSize, query.PageNumber);

            return await Task.FromResult(result);
        }

        public async Task<PagedResult<OrderDto>> GetAllPagedOrdersWithOptionalFilters(GetAllPagedOrders query)
        {
            PageSizeAndNumberValidate(query.PageNumber, query.PageSize);

            if (query.SearchPhrase=="all")
            {
                query.SearchPhrase = null;
            }

            IQueryable<OrderDocument> baseQuery;

            if (query.SearchPhrase is null)
            {
                baseQuery = _ordersRepository.Collection
                .AsQueryable();
            }
            else
            {
                baseQuery = _ordersRepository.Collection
                .AsQueryable()
                .Where(x => x.CompanyName.ToLower().Contains(query.SearchPhrase.ToLower()));
            }



            var orderdocuments = baseQuery.Skip(query.PageSize * (query.PageNumber-1))
                .Take(query.PageSize)?.ToList();

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnSelector = new Dictionary<string, Expression<Func<OrderDocument, object>>>
                {
                    { nameof(OrderDocument.CreatedAt).ToLower(), x => x.CreatedAt },
                    { nameof(OrderDocument.OrderState).ToLower(), x => x.OrderState },
                    { nameof(OrderDocument.CompanyName).ToLower(), x => x.CompanyName },
                };

                var selectedColumn = columnSelector[query.SortBy.ToLower()];

                switch (query.SortDirection)
                {
                    case SortDirection.ASC:
                        orderdocuments.OrderBy(x => selectedColumn);
                        break;
                    default:
                        orderdocuments.OrderByDescending(x=> selectedColumn);
                        break;
                }

            }

            var orders = orderdocuments.Select(x=>x.AsDto());

            var totalItemsCount = orders.Count();

            var result = new PagedResult<OrderDto>(orders.ToList(), totalItemsCount, query.PageSize, query.PageNumber);

            return await Task.FromResult(result);
        }

        private void PageSizeAndNumberValidate(int pageNumber, int pageSize)
        {
            var availablePageSizes = new int[] { 5, 10, 50 };

            if (pageNumber>pageSize)
            {
                throw new InvalidPageNumberException();
            }

            if (!availablePageSizes.Contains(pageSize))
            {
                throw new InvalidPageSizeException();
            }

        }

        public async Task<IEnumerable<OrderDto>> GetCustomerOrders(Guid customerId)
        {
            var orders = (await _ordersRepository
                .FindAsync(x => x.CustomerId == customerId))
                .Select(x => x.AsBusiness());

            return await Task.FromResult(orders.Select(x => x?.AsDto()));
            
        }

        public async Task<OrderDetailsDto> GetOrderDetails(Guid orderId)
        {
            var order = (await _ordersRepository
                .GetAsync(orderId))?
                .AsBusiness();
                
            return await Task.FromResult(order?.AsDetailsDto());
        }
    }
}
