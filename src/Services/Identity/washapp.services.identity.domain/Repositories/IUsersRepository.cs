using washapp.services.identity.domain.Entities;

namespace washapp.services.identity.domain.Repositories;

public interface IUsersRepository
{
    Task<User> GetByIdAsync(Guid userId);
    Task<User> GetByLogin(string login);
    Task<ICollection<User>>GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}