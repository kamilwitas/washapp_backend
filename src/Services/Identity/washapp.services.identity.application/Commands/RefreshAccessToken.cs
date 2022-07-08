using MediatR;
using washapp.services.identity.application.DTO;

namespace washapp.services.identity.application.Commands;

public class RefreshAccessToken : IRequest<AuthDto>
{
    public string ExpiredAccessToken { get; set; }
    public string RefreshToken { get; set; }
}