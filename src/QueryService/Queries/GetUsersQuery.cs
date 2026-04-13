using MediatR;
using Users.Example.Models.Dtos.Users;

namespace Users.Example.QueryService.Queries;

public class GetUsersQuery : IRequest<IEnumerable<UserDto>>
{
}