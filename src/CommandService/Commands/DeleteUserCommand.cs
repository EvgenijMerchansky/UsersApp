using MediatR;

namespace Users.Example.CommandService.Commands;

public class DeleteUserCommand(int userId) : IRequest
{
    public int UserId { get; set; } = userId;
}