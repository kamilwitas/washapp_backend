using washapp.services.identity.domain.Entities;
using washapp.services.identity.domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using washapp.services.identity.application.Exceptions;

namespace washapp.services.identity.application.Commands.Handlers;

public class ChangeRoleHandler : IRequestHandler<ChangeRole>
{
    private readonly ILogger<ChangeRoleHandler> _logger;
    private readonly IUsersRepository _usersRepository;

    public ChangeRoleHandler(ILogger<ChangeRoleHandler> logger, IUsersRepository usersRepository)
    {
        _logger = logger;
        _usersRepository = usersRepository;
    }

    public async Task<Unit> Handle(ChangeRole request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(request.UserId);
        if (user is null)
        {
            throw new UserDoesNotExistsException(request.UserId);
        } 
        var isRoleExists=Enum.TryParse(request.NewRole, out Roles role);

        if (!isRoleExists)
        {
            throw new RoleDoesNotExistsException();
        }

        if (user.Role == role)
        {
            return Unit.Value;
        }
        
        user.ChangeRole(role);
        await _usersRepository.UpdateAsync(user);
        _logger.LogInformation($"Permissions have been changed for user with id: {request.UserId}. New role: {request.NewRole}");
        return Unit.Value;
    }
}