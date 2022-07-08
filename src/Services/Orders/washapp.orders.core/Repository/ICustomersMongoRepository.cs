using washapp.orders.core.Entities;

namespace washapp.orders.core.Repository
{
    public interface ICustomersMongoRepository
    {
        Task AddAsync(Customer customer);
        Task<Customer> GetCustomerAsync(Guid customerId);
        Task UpdateAsync(Customer customer);
    }
}
