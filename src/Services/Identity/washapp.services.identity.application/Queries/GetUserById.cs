using MediatR;
using washapp.services.identity.application.DTO;

namespace washapp.services.identity.application.Queries;

public class GetUserById : IRequest<GetUserDto>
{
    public Guid UserId { get; set; }
}