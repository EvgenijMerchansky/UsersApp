using MediatR;
using Users.Example.Models.Dtos.Users;

namespace Users.Example.CommandService.Commands;

public class CreateUserCommand(CreateUserDto createUserDto) : IRequest
{
    public CreateUserDto User { get; set; } = createUserDto;
}