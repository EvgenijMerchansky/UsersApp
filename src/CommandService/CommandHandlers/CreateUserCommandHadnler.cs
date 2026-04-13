using MediatR;
using Users.Example.CommandService.Commands;
using Users.Example.Services.Services;

namespace Users.Example.CommandService.CommandHandlers;

public class CreateUserCommandHadnler(IUserService userService) : IRequestHandler<CreateUserCommand>
{
    public async Task Handle(CreateUserCommand command, CancellationToken ct = default)
    {
        await userService.Create(command.User, ct);
    }
}