using MediatR;

namespace washapp.services.identity.application.Commands;

public class DeleteUser : IRequest
{
    public Guid UserId { get; set; }
    
}