using MediatR;
using Users.Example.Models.Dtos.Users;

namespace Users.Example.QueryService.Queries;

public class GetUserQuery(int userId) : IRequest<UserDto>
{
    public int UserId { get; set; } = userId;
}