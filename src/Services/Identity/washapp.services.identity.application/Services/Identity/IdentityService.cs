using System.Security.Claims;
using washapp.services.identity.domain.Entities;
using washapp.services.identity.domain.Repositories;
using Microsoft.AspNetCore.Identity;
using washapp.services.identity.application.DTO;
using washapp.services.identity.application.Exceptions;

namespace washapp.services.identity.application.Services.Identity;

public class IdentityService : IIdentityService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public IdentityService(IUsersRepository usersRepository, IPasswordHasher<User> passwordHasher, IJwtProvider jwtProvider)
    {
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task<AuthDto> SignInAsync(string login, string password)
    {
        var user = await _usersRepository.GetByLogin(login);

        if (user is null)
        {
            throw new UserDoesNotExistsException();
        }
        
        var passwordVerification = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

        if (passwordVerification==PasswordVerificationResult.Failed)
        {
            throw new InvalidLoginCredentialsException();
        }

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName),
            new Claim(ClaimTypes.GivenName,user.Login),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };
        DateTime expirationDateTime;
        var accessToken = _jwtProvider.GetAuthorizationToken(claims,out expirationDateTime );
        var refreshToken = await _jwtProvider.GenerateRefreshToken();
        
        user.SetRefreshToken(refreshToken, DateTime.UtcNow.AddDays(1));
        await _usersRepository.UpdateAsync(user);

        var authDto = new AuthDto()
        {
            AccessToken = accessToken,
            Expires = expirationDateTime,
            RefreshToken = refreshToken
        };

        return authDto;
    }

    public async Task<AuthDto>RefreshToken(string expiredAccessToken, string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(expiredAccessToken) || string.IsNullOrWhiteSpace(refreshToken))
        {
            throw new InvalidRefreshTokenRequestException();
        }

        var principal = await _jwtProvider.GetPrincipalFromExpiredToken(expiredAccessToken);

        if (principal.Claims is null)
        {
            throw new InvalidAccessTokenOrRefreshTokenException();
        }

        var userId = principal.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

        var user = await _usersRepository.GetByIdAsync(Guid.Parse(userId));

        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryDate<=DateTime.UtcNow)
        {
            throw new InvalidAccessTokenOrRefreshTokenException();
        }

        var newAccessToken = _jwtProvider.GetAuthorizationToken(principal.Claims.ToList(), out DateTime expires);
        var newRefreshToken = await _jwtProvider.GenerateRefreshToken();
        
        user.SetRefreshToken(newRefreshToken,DateTime.UtcNow.AddDays(1));
        await _usersRepository.UpdateAsync(user);

        var authDto = new AuthDto()
        {
            AccessToken = newAccessToken,
            Expires = expires,
            RefreshToken = newRefreshToken
        };

        return authDto;
    }

    public async Task RevokeToken(Guid userId)
    {
        var user = await _usersRepository.GetByIdAsync(userId);

        if (user is null)
        {
            throw new UserDoesNotExistsException(userId);
        }
        user.RevokeRefreshToken();
        await _usersRepository.UpdateAsync(user);
    }

    public async Task RevokeAllTokens()
    {
        var users = await _usersRepository.GetAllAsync();

        if (users is null)
        {
            throw new UserDoesNotExistsException();
        }
        
        foreach (var user in users)
        {
            user.RevokeRefreshToken();
            await _usersRepository.UpdateAsync(user);
        }
    }
}