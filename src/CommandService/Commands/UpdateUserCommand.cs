using MediatR;
using Users.Example.Models.Dtos.Users;

namespace Users.Example.CommandService.Commands
{
    public class UpdateUserCommand(UpdateUserDto updateUserDto) : IRequest
    {
        public UpdateUserDto User { get; set; } = updateUserDto;
    }
}