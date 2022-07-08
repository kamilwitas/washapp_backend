using washapp.services.customers.domain.Entities;
using washapp.services.customers.domain.Repository;
using Microsoft.EntityFrameworkCore;
using washapp.services.customers.infrastructure.Database;

namespace washapp.services.customers.infrastructure.Repositories;

public class AssortmentCategoriesRepository : IAssortmentCategoriesRepository
{
    private readonly WashAppDbContext _dbContext;

    public AssortmentCategoriesRepository(WashAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<AssortmentCategory>> GetAllAsync()
    {
        var categories = await _dbContext.AssortmentCategories.ToListAsync();
        return categories;
    }

    public async Task<AssortmentCategory> GetByIdAsync(Guid categoryId)
    {
        var category = await _dbContext.AssortmentCategories.FirstOrDefaultAsync(x => x.Id == categoryId);
        return category;
    }

    public async Task<AssortmentCategory> GetByName(string categoryName)
    {
        var category = await _dbContext.AssortmentCategories
            .FirstOrDefaultAsync(x => x.CategoryName.ToLower() == categoryName.ToLower());
        return category;
    }

    public async Task AddAsync(AssortmentCategory category)
    {
        await _dbContext.AssortmentCategories.AddAsync(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(AssortmentCategory category)
    {
        _dbContext.AssortmentCategories.Update(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid categoryId)
    {
        var category = await _dbContext.AssortmentCategories
            .FirstOrDefaultAsync(x => x.Id == categoryId);

        _dbContext.AssortmentCategories.Remove(category);
        await _dbContext.SaveChangesAsync();
    }
}