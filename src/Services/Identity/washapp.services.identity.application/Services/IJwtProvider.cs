using System.Security.Claims;
using washapp.services.identity.application.DTO;
using washapp.services.identity.domain.Entities;

namespace washapp.services.identity.application.Services;

public interface IJwtProvider
{
    string GetAuthorizationToken(IEnumerable<Claim>claims, out DateTime expires);
    Task<string> GenerateRefreshToken();
    Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string expiredToken);
}