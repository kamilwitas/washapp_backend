using MediatR;
using washapp.services.identity.application.DTO;

namespace washapp.services.identity.application.Commands;

public class SignIn : IRequest<AuthDto>
{
    public string Login { get; set; }
    public string Password { get; set; }
}