using MediatR;
using Microsoft.Extensions.Logging;
using washapp.services.identity.application.Services;

namespace washapp.services.identity.application.Commands.Handlers;

public class RevokeUserTokenHandler : IRequestHandler<RevokeUserToken>
{
    private readonly ILogger<RevokeUserTokenHandler> _logger;
    private readonly IIdentityService _identityService;

    public RevokeUserTokenHandler(ILogger<RevokeUserTokenHandler> logger, IIdentityService identityService)
    {
        _logger = logger;
        _identityService = identityService;
    }

    public async Task<Unit> Handle(RevokeUserToken request, CancellationToken cancellationToken)
    {
        await _identityService.RevokeToken(request.UserId);
        _logger.LogInformation($"Refresh token fo user with id: {request.UserId} has been revoked");
        return Unit.Value;
        
    }
}