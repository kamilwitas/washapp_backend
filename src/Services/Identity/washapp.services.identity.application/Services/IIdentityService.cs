using System.Security.Claims;
using washapp.services.identity.application.DTO;

namespace washapp.services.identity.application.Services;

public interface IIdentityService
{
    Task<AuthDto> SignInAsync(string login, string password);
    Task<AuthDto> RefreshToken (string expiredAccessToken, string refreshToken);
    Task RevokeToken(Guid userId);
    Task RevokeAllTokens();

}