using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using washapp.services.identity.application.Exceptions;
using washapp.services.identity.application.Services;
using Microsoft.IdentityModel.Tokens;
using washapp.services.identity.infrastructure.Auth.Settings;

namespace washapp.services.identity.infrastructure.Services;

public class JwtProvider : IJwtProvider
{
    private readonly AuthenticationSettings _authenticationSettings;

    public JwtProvider(AuthenticationSettings authenticationSettings)
    {
        _authenticationSettings = authenticationSettings;
    }

    public string GetAuthorizationToken(IEnumerable<Claim> claims, out DateTime expires)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new JwtSecurityToken(
            _authenticationSettings.Issuer,
            _authenticationSettings.Issuer,
            claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: credentials);
        expires = tokenDescriptor.ValidTo;
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(tokenDescriptor);
    }

    public async Task<string> GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        
        return await Task.FromResult(
                Convert.ToBase64String(randomNumber)
            );
        
    }

    public async Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string expiredToken)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.Key)),
            ValidateLifetime = false,
            ValidIssuer = _authenticationSettings.Issuer,
            ValidAudience = _authenticationSettings.Issuer
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var principal = tokenHandler.ValidateToken(expiredToken, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new InvalidAccessTokenException();

            return await Task.FromResult(principal);
        }
        catch (Exception e)
        {
            throw new InvalidAccessTokenOrRefreshTokenException();
        }
        
        
    }
}