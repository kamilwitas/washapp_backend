using washapp.services.customers.domain.Entities;
using washapp.services.customers.domain.Repository;
using Microsoft.EntityFrameworkCore;
using washapp.services.customers.infrastructure.Database;

namespace washapp.services.customers.infrastructure.Repositories;

public class CustomersRepository : ICustomersRepository
{
    private readonly WashAppDbContext _washAppDbContext;

    public CustomersRepository(WashAppDbContext washAppDbContext)
    {
        _washAppDbContext = washAppDbContext;
    }

    public async Task<Customer> GetAsync(Guid customerId)
    {
        var customer = await _washAppDbContext.Customers
            .Include(x => x.Address)
            .ThenInclude(x => x.Location)
            .Include(x => x.AssortmentItems)
            .ThenInclude(x => x.AssortmentCategory)
            .FirstOrDefaultAsync(x => x.Id == customerId);
        
        return customer;
    }

    public async Task AddAsync(Customer customer)
    {
        await _washAppDbContext.Customers.AddRangeAsync(customer);
        await _washAppDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer customer)
    {
        _washAppDbContext.Customers.UpdateRange(customer);
        await _washAppDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid customerId)
    {
        var customer = await _washAppDbContext.Customers
            .Include(x => x.Address)
            .ThenInclude(x => x.Location)
            .Include(x => x.AssortmentItems)
            .ThenInclude(x => x.AssortmentCategory)
            .FirstOrDefaultAsync(x => x.Id == customerId);
        _washAppDbContext.Assortments.RemoveRange(customer.AssortmentItems);
        _washAppDbContext.Addresses.RemoveRange(customer.Address);
        _washAppDbContext.Customers.RemoveRange(customer);
        await _washAppDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Customer>> GetByLocationAsync(Guid locationId)
    {
        var customer = await _washAppDbContext.Customers
            .Include(x => x.Address)
            .ThenInclude(x => x.Location)
            .Include(x => x.AssortmentItems)
            .ThenInclude(x => x.AssortmentCategory)
            .Where(x => x.Address.Location.Id == locationId)
            .ToListAsync();
        
        return customer;
    }

    public async Task<IEnumerable<Customer>> GetAll()
    {
        var customers = await _washAppDbContext.Customers
            .Include(x => x.Address)
            .ThenInclude(x => x.Location)
            .Include(x => x.AssortmentItems)
            .ThenInclude(x => x.AssortmentCategory)
            .ToListAsync();
        return customers;
    }

    public async Task<Customer> GetByExpression(Func<Customer, bool> predicate)
    {
        var customer = _washAppDbContext.Customers
            .Include(x => x.Address)
            .ThenInclude(x => x.Location)
            .Include(x => x.AssortmentItems)
            .ThenInclude(x => x.AssortmentCategory)
            .Where(predicate)
            .FirstOrDefault();

        
        return await Task.FromResult(customer);

    }
}