using MediatR;
using washapp.services.identity.application.DTO;

namespace washapp.services.identity.application.Queries;

public class GetAllUsers : IRequest<IEnumerable<GetUserDto>>
{
    
}