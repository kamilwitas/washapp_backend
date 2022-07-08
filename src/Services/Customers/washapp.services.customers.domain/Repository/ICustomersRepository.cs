using washapp.services.customers.domain.Entities;

namespace washapp.services.customers.domain.Repository;

public interface ICustomersRepository
{
    Task<Customer> GetAsync(Guid customerId);
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(Guid customerId);
    Task<IEnumerable<Customer>> GetByLocationAsync(Guid locationId);
    Task<IEnumerable<Customer>> GetAll();
    Task<Customer> GetByExpression(Func<Customer, bool> predicate);
}