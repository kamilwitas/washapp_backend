using washapp.services.identity.domain.Entities;
using MediatR;

namespace washapp.services.identity.application.Commands;

public class ChangeRole : IRequest
{
    public Guid UserId { get; set; }
    public string NewRole { get; set; }
}