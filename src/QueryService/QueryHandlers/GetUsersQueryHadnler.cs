using MediatR;
using Users.Example.Models.Dtos.Users;
using Users.Example.QueryService.Queries;
using Users.Example.Services.Services;

namespace Users.Example.QueryService.QueryHandlers;

public class GetUsersQueryHadnler(IUserService userService) : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    public Task<IEnumerable<UserDto>> Handle(GetUsersQuery query, CancellationToken ct = default)
    {
        return userService.GetAll(ct);
    }
}