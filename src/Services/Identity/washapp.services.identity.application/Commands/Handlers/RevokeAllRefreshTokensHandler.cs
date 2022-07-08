using MediatR;
using Microsoft.Extensions.Logging;
using washapp.services.identity.application.Services;

namespace washapp.services.identity.application.Commands.Handlers;

public class RevokeAllRefreshTokensHandler : IRequestHandler<RevokeAllRefreshTokens>
{
    private readonly ILogger<RevokeAllRefreshTokensHandler> _logger;
    private readonly IIdentityService _identityService;

    public RevokeAllRefreshTokensHandler(ILogger<RevokeAllRefreshTokensHandler> logger, IIdentityService identityService)
    {
        _logger = logger;
        _identityService = identityService;
    }

    public async Task<Unit> Handle(RevokeAllRefreshTokens request, CancellationToken cancellationToken)
    {
        await _identityService.RevokeAllTokens();
        _logger.LogInformation("All refresh tokens have been revoked");
        return Unit.Value;
    }
}