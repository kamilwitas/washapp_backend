using washapp.services.identity.domain.Entities;
using washapp.services.identity.domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using washapp.services.identity.application.DTO;
using washapp.services.identity.application.Exceptions;

namespace washapp.services.identity.application.Commands.Handlers;

public class RegisterUserHandler : IRequestHandler<RegisterUser>
{
    private readonly IUsersRepository _usersRepository;
    private readonly ILogger<RegisterUserHandler> _logger;
    private readonly IValidator<RegisterUserDto> _validator;
    private readonly IPasswordHasher<User> _passwordHasher;

    public RegisterUserHandler(IUsersRepository usersRepository, ILogger<RegisterUserHandler> logger, 
        IValidator<RegisterUserDto>validator ,IPasswordHasher<User> passwordHasher)
    {
        _usersRepository = usersRepository;
        _logger = logger;
        _validator = validator;
        _passwordHasher = passwordHasher;
    }

    public async Task<Unit> Handle(RegisterUser request, CancellationToken cancellationToken)
    {
        var registerRequestValidationResult = await _validator.ValidateAsync(request.RegisterUserRequest);
        if (!registerRequestValidationResult.IsValid)
        {
            throw new ValidationErrorException(registerRequestValidationResult.Errors);
        }

        var user = new User(
            Guid.NewGuid(),
            request.RegisterUserRequest.FirstName,
            request.RegisterUserRequest.LastName,
            request.RegisterUserRequest.Login,
            DateTime.Now, 
            Roles.User
        );
        var hashedPassword = _passwordHasher.HashPassword(user, request.RegisterUserRequest.Password);
        user.SetPassword(hashedPassword);
        await _usersRepository.AddAsync(user);
        _logger.LogInformation($"User with id: {user.Id} was created");
        return Unit.Value;
        ;
    }
}