using washapp.services.customers.domain.Entities;
using washapp.services.customers.domain.Repository;
using Microsoft.EntityFrameworkCore;
using washapp.services.customers.infrastructure.Database;

namespace washapp.services.customers.infrastructure.Repositories;

public class LocationsRepository : ILocationsRepository
{
    private readonly WashAppDbContext _dbContext;

    public LocationsRepository(WashAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Location>> GetAllAsync()
    {
        var locations =await _dbContext.Locations.ToListAsync();
        return locations;
    }

    public async Task<Location> GetByIdAsync(Guid locationId)
    {
        var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.Id == locationId);
        return location;
    }

    public async Task<IEnumerable<Location>> GetByLocationName(string locationName)
    {
        var locations = await _dbContext.Locations.Where(x => x.LocationName == locationName).ToListAsync();
        return locations;
    }

    public async Task AddAsync(Location location)
    {
        await _dbContext.Locations.AddAsync(location);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Location location)
    {
        _dbContext.Locations.Update(location);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid locationId)
    {
        var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.Id == locationId);
        _dbContext.Locations.Remove(location);
        await _dbContext.SaveChangesAsync();
    }
}