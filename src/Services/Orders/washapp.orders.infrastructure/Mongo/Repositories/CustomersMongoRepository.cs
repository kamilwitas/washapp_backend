using washapp.orders.core.Entities;
using washapp.orders.core.Repository;
using washapp.orders.infrastructure.Mongo.Mappings;
using Convey.Persistence.MongoDB;
using washapp.orders.infrastructure.Mongo.Documents;

namespace washapp.orders.infrastructure.Mongo.Repositories
{
    public class CustomersMongoRepository : ICustomersMongoRepository
    {
        private readonly IMongoRepository<CustomerDocument,Guid> _customersRepository;

        public CustomersMongoRepository(IMongoRepository<CustomerDocument, Guid> customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task AddAsync(Customer customer)
        {
            await _customersRepository.AddAsync(customer?.AsDocument());
        }

        public async Task<Customer> GetCustomerAsync(Guid customerId)
        {
            var customer = await _customersRepository.GetAsync(customerId);

            return customer?.AsBusiness();
        }

        public async Task UpdateAsync(Customer customer)
        {
            await _customersRepository.UpdateAsync(customer?.AsDocument());
        }
    }
}
