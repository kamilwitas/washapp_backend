using MediatR;
using washapp.services.identity.application.DTO;
using washapp.services.identity.application.Services;

namespace washapp.services.identity.application.Commands.Handlers
{
    public class SignInHandler : IRequestHandler<SignIn,AuthDto>
    {
        private readonly IIdentityService _identityService;

        public SignInHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<AuthDto> Handle(SignIn request, CancellationToken cancellationToken)
        {
            var authDto = await _identityService.SignInAsync(request.Login, request.Password);
            return authDto;
        }
    }
}