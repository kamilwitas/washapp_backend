using washapp.services.identity.domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using washapp.services.identity.application.Exceptions;

namespace washapp.services.identity.application.Commands.Handlers;

public class DeleteUserHandler : IRequestHandler<DeleteUser>
{
    private readonly IUsersRepository _usersRepository;
    private readonly ILogger<DeleteUserHandler> _logger;

    public DeleteUserHandler(IUsersRepository usersRepository, ILogger<DeleteUserHandler> logger)
    {
        _usersRepository = usersRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteUser request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(request.UserId);
        if (user is null)
        {
            throw new UserDoesNotExistsException(request.UserId);
        }

        await _usersRepository.DeleteAsync(user);
        
        _logger.LogInformation($"User with id: {request.UserId} was deleted");
        
        return Unit.Value;
    }
}