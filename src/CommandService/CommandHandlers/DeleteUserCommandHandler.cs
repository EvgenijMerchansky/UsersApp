using MediatR;
using Users.Example.CommandService.Commands;
using Users.Example.Services.Services;

namespace Users.Example.CommandService.CommandHandlers;

public class DeleteUserCommandHandler(IUserService userService) : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand command, CancellationToken ct = default)
    {
        await userService.Delete(command.UserId, ct);
    }
}