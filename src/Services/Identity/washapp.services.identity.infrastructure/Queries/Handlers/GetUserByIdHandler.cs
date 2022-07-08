using washapp.services.identity.application.DTO;
using washapp.services.identity.application.Exceptions;
using washapp.services.identity.application.Queries;
using washapp.services.identity.domain.Repositories;
using MediatR;

namespace washapp.services.identity.infrastructure.Queries.Handlers;

public class GetUserByIdHandler : IRequestHandler<GetUserById, GetUserDto>
{
    private readonly IUsersRepository _usersRepository;

    public GetUserByIdHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<GetUserDto> Handle(GetUserById request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            throw new UserDoesNotExistsException(request.UserId);
        }

        return new GetUserDto(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Login,
            user.Role
        );

    }
}