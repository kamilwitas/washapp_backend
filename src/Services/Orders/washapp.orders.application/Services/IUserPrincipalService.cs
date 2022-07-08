using washapp.orders.application.DTO;

namespace washapp.orders.application.Services
{
    public interface IUserPrincipalService
    {
        Task<UserPrincipal> GetUserPrincipal();

    }
}
