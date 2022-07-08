using washapp.services.customers.domain.Entities;

namespace washapp.services.customers.domain.Repository;

public interface IAssortmentCategoriesRepository
{
    Task<IEnumerable<AssortmentCategory>> GetAllAsync();
    Task<AssortmentCategory> GetByIdAsync(Guid categoryId);
    Task<AssortmentCategory> GetByName(string categoryName);
    Task AddAsync(AssortmentCategory category);
    Task UpdateAsync(AssortmentCategory category);
    Task DeleteAsync(Guid categoryId);
}