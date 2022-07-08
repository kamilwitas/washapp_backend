using System.Security.Claims;
using washapp.services.identity.domain.Entities;
using washapp.services.identity.domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using washapp.services.identity.application.Exceptions;

namespace washapp.services.identity.application.Commands.Handlers;

public class ChangePasswordHandler : IRequestHandler<ChangePassword>
{
    private readonly IUsersRepository _usersRepository;
    private IPasswordHasher<User> _passwordHasher;
    private IValidator<ChangePassword> _validator;
    private IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<ChangePasswordHandler> _logger;

    public ChangePasswordHandler(IUsersRepository usersRepository, IPasswordHasher<User> passwordHasher, 
        IValidator<ChangePassword> validator, IHttpContextAccessor httpContextAccessor,
        ILogger<ChangePasswordHandler>logger)
    {
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
        _validator = validator;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task<Unit> Handle(ChangePassword request, CancellationToken cancellationToken)
    {
        var authenticatedUser = _httpContextAccessor.HttpContext?
            .User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName).Value;

        if (authenticatedUser != request.Login)
        {
            throw new NotAuthorizedException();
        }

        var user = await _usersRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            throw new UserDoesNotExistsException(request.UserId);
        }

        var validationResults = await _validator.ValidateAsync(request);
        
        if (!validationResults.IsValid)
        {
            throw new ValidationErrorException(validationResults.Errors);
        }

        var newHashedPassword = _passwordHasher.HashPassword(user, request.NewPassword);
        user.SetPassword(newHashedPassword);
        await _usersRepository.UpdateAsync(user);
        _logger.LogInformation($"User with id: {user.Id} has changed password");
        return Unit.Value;
    }
}