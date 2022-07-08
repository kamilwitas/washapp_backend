using System.Security.Claims;
using washapp.services.identity.application.DTO;
using washapp.services.identity.application.Exceptions;
using washapp.services.identity.application.Queries;
using washapp.services.identity.domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace washapp.services.identity.infrastructure.Queries.Handlers;

public class GetAuthorizedUserHandler : IRequestHandler<GetAuthorizedUser,GetUserDto>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUsersRepository _usersRepository;

    public GetAuthorizedUserHandler(IHttpContextAccessor httpContextAccessor,IUsersRepository usersRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _usersRepository = usersRepository;
    }

    public async Task<GetUserDto> Handle(GetAuthorizedUser request, CancellationToken cancellationToken)
    {
        var userId =
            _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

        var user = await _usersRepository.GetByIdAsync(Guid.Parse(userId));

        if (user is null)
        {
            throw new UserDoesNotExistsException(Guid.Parse(userId));
        }

        return new GetUserDto(user.Id, user.FirstName, user.LastName, user.Login, user.Role);
    }
}