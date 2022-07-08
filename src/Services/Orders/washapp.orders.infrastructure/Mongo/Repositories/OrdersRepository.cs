using washapp.orders.core.Entities;
using washapp.orders.core.Repository;
using washapp.orders.infrastructure.Mongo.Mappings;
using Convey.Persistence.MongoDB;
using washapp.orders.infrastructure.Mongo.Documents;

namespace washapp.orders.infrastructure.Mongo.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IMongoRepository<OrderDocument,Guid> _ordersRepository;

        public OrdersRepository(IMongoRepository<OrderDocument, Guid> ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task AddAsync(Order order)
        {
            await _ordersRepository.AddAsync(order.AsDocument());
        }

        public async Task DeleteAsync(Guid orderId)
        {
            await _ordersRepository.DeleteAsync(orderId);
        }

        public async Task<Order> GetAsync(Guid orderId)
        {
            var order = await _ordersRepository.GetAsync(orderId);
            return order?.AsBusiness();
        }

        public async Task UpdateAsync(Order order)
        {
            await _ordersRepository.UpdateAsync(order.AsDocument());
        }
    }
}
