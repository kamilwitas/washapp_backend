using washapp.services.customers.domain.Entities;
using washapp.services.customers.domain.Repository;
using Microsoft.EntityFrameworkCore;
using washapp.services.customers.infrastructure.Database;

namespace washapp.services.customers.infrastructure.Repositories;

public class AssortmentsRepository : IAssortmentsRepository
{
    private readonly WashAppDbContext _washAppDbContext;

    public AssortmentsRepository(WashAppDbContext washAppDbContext)
    {
        _washAppDbContext = washAppDbContext;
    }

    public async Task<IEnumerable<Assortment>> GetAssortmentsByCustomer(Guid customerId)
    {
        var customer = await _washAppDbContext.Customers
            .Include(x => x.AssortmentItems)
            .ThenInclude(x=>x.AssortmentCategory)
            .FirstOrDefaultAsync(x => x.Id == customerId);
        return customer?.AssortmentItems;
    }

    public async Task<IEnumerable<Assortment>> GetAssortmentByExpression(Func<Assortment, bool> predicate)
    {
        var assortments = _washAppDbContext.Assortments
            .Include(x=>x.AssortmentCategory)
            .Where(predicate).ToList();
        return await Task.FromResult(assortments);
    }


    public async Task UpdateAsync(Assortment assortment)
    {
        _washAppDbContext.Assortments.UpdateRange(assortment);
        await _washAppDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid assortmentId)
    {
        var assortment = await _washAppDbContext.Assortments
            .Include(x => x.AssortmentCategory)
            .FirstOrDefaultAsync(x => x.Id == assortmentId);
        _washAppDbContext.Assortments.RemoveRange(assortment);
        await _washAppDbContext.SaveChangesAsync();
    }
}