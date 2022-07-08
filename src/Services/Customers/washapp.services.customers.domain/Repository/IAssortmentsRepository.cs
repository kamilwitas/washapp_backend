using washapp.services.customers.domain.Entities;

namespace washapp.services.customers.domain.Repository;

public interface IAssortmentsRepository
{
    Task<IEnumerable<Assortment>> GetAssortmentsByCustomer(Guid customerId);
    Task<IEnumerable<Assortment>> GetAssortmentByExpression(Func<Assortment,bool> predicate);
    Task UpdateAsync(Assortment assortment);
    Task DeleteAsync(Guid assortmentId);
}