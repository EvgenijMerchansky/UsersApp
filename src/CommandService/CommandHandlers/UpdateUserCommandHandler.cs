using MediatR;
using Users.Example.CommandService.Commands;
using Users.Example.Services.Services;

namespace Users.Example.CommandService.CommandHandlers;

public class UpdateUserCommandHandler(IUserService userService) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand command, CancellationToken ct = default)
    {
        await userService.Update(command.User, ct);
    }
}