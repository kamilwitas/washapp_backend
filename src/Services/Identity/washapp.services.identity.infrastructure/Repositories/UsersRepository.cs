using washapp.services.identity.domain.Entities;
using washapp.services.identity.domain.Repositories;
using Microsoft.EntityFrameworkCore;
using washapp.services.identity.infrastructure.Data;

namespace washapp.services.identity.infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly WashAppDbContext _washAppDbContext;

    public UsersRepository(WashAppDbContext washAppDbContext)
    {
        _washAppDbContext = washAppDbContext;
    }

    public async Task<User> GetByIdAsync(Guid userId)
    {
        var user = await _washAppDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        return user;
    }

    public async Task<User> GetByLogin(string login)
    {
        var user = await _washAppDbContext.Users.FirstOrDefaultAsync(x => x.Login == login);
        return user;
    }

    public async Task<ICollection<User>> GetAllAsync()
    {
        var users = await _washAppDbContext.Users.ToListAsync();
        return users;
    }

    public async Task AddAsync(User user)
    {
        await _washAppDbContext.AddAsync(user);
        await _washAppDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    { 
        _washAppDbContext.Users.Update(user);
        await _washAppDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _washAppDbContext.Users.Remove(user);
        await _washAppDbContext.SaveChangesAsync();
    }
}