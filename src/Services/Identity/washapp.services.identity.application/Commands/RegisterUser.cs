using MediatR;
using washapp.services.identity.application.DTO;

namespace washapp.services.identity.application.Commands;

public class RegisterUser : IRequest
{
    public RegisterUserDto RegisterUserRequest { get; }

    public RegisterUser(RegisterUserDto registerUserDto)
    {
        RegisterUserRequest = registerUserDto;
    }
}