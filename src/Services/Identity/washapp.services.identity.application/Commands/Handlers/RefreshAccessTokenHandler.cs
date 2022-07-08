using MediatR;
using washapp.services.identity.application.DTO;
using washapp.services.identity.application.Services;

namespace washapp.services.identity.application.Commands.Handlers;

public class RefreshAccessTokenHandler : IRequestHandler<RefreshAccessToken,AuthDto>
{
    private readonly IIdentityService _identityService;

    public RefreshAccessTokenHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<AuthDto> Handle(RefreshAccessToken request, CancellationToken cancellationToken)
    {
        var authDto = await _identityService.RefreshToken(request.ExpiredAccessToken, request.RefreshToken);
        return authDto;
    }
}