using MediatR;
using Users.Example.Models.Dtos.Users;

namespace Users.Example.CommandService.Commands
{
    public class UpdateUserCommand(int userId, UserDto userDto) : IRequest
    {
        public int UserId { get; set; } = userId;
        public UserDto User { get; set; } = userDto;
    }
}