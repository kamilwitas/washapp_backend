using washapp.services.identity.application.DTO;
using washapp.services.identity.application.Queries;
using washapp.services.identity.domain.Repositories;
using MediatR;

namespace washapp.services.identity.infrastructure.Queries.Handlers;

public class GetAllUsersHandler : IRequestHandler<GetAllUsers,IEnumerable<GetUserDto>>
{
    private readonly IUsersRepository _usersRepository;

    public GetAllUsersHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<IEnumerable<GetUserDto>> Handle(GetAllUsers request, CancellationToken cancellationToken)
    {
        var users = await _usersRepository.GetAllAsync();
        
        return users?.Select(x =>
            new GetUserDto(x.Id, x.FirstName, x.LastName, x.Login, x.Role))
        .ToList();
    }
}