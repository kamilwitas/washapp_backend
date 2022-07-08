using washapp.services.customers.domain.Entities;

namespace washapp.services.customers.domain.Repository;

public interface ILocationsRepository
{
    Task<IEnumerable<Location>> GetAllAsync();
    Task<Location> GetByIdAsync(Guid locationId);
    Task<IEnumerable<Location>> GetByLocationName(string locationName);
    Task AddAsync(Location location);
    Task UpdateAsync(Location location);
    Task DeleteAsync(Guid locationId);
}