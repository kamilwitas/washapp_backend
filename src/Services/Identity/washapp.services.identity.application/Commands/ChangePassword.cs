using MediatR;

namespace washapp.services.identity.application.Commands;

public class ChangePassword : IRequest
{
    public Guid UserId { get; set; }
    public string Login { get; set; }
    public string NewPassword { get; set; }
    public string NewPasswordConfirmation { get; set; }
}