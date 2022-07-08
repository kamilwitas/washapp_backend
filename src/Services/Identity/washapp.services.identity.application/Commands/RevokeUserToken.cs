using MediatR;

namespace washapp.services.identity.application.Commands;

public class RevokeUserToken : IRequest
{
    public Guid UserId { get; set; }
}