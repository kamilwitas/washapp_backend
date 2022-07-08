using MediatR;

namespace washapp.services.identity.application.Commands;

public class UpdateUser : IRequest
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Login { get; set; } 
}