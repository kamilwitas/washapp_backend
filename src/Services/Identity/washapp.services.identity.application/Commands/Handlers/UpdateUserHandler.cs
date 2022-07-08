using washapp.services.identity.domain.Entities;
using washapp.services.identity.domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using washapp.services.identity.application.Exceptions;

namespace washapp.services.identity.application.Commands.Handlers;

public class UpdateUserHandler : IRequestHandler<UpdateUser>
{
    private readonly IUsersRepository _usersRepository;
    private readonly ILogger<UpdateUserHandler> _logger;

    public UpdateUserHandler(IUsersRepository usersRepository, ILogger<UpdateUserHandler> logger)
    {
        _usersRepository = usersRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateUser request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            throw new UserDoesNotExistsException(request.UserId);
        }

        if (! await IsLoginAvailable(user,request))
        {
            throw new LoginIsAlreadyInUseException(request.Login);
        }
        
        if (IsUserUpdated(user,request))
        {
            User updatedUser = new User(request.FirstName, request.LastName, request.Login, DateTime.UtcNow);
            user.Update(updatedUser);
            await _usersRepository.UpdateAsync(user);
            _logger.LogInformation($"User with id: {user.Id} has been updated");
        }
        return Unit.Value;
    }

    private bool IsUserUpdated(User origUser, UpdateUser request)
    {
        if (origUser.FirstName != request.FirstName ||
            origUser.LastName != request.LastName ||
            origUser.Login != request.Login)
        {
            return true;
        }
        else
            return false;
    }

    private async Task<bool> IsLoginAvailable(User origUser, UpdateUser request)
    {
        if (origUser.Login != request.Login)
        {
            var duplicateUser = await _usersRepository.GetByLogin(request.Login);
            if (duplicateUser is not null)
            {
                return false;
            }
        }

        return true;
    }
}