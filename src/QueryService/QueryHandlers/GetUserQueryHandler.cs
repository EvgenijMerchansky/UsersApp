using MediatR;
using Users.Example.Models.Dtos.Users;
using Users.Example.QueryService.Queries;
using Users.Example.Services.Services;

namespace Users.Example.QueryService.QueryHandlers;

public class GetUserQueryHandler(IUserService userService) : IRequestHandler<GetUserQuery, UserDto>
{
    public Task<UserDto> Handle(GetUserQuery query, CancellationToken ct = default)
    {
        return userService.Get(query.UserId, ct);
    }
}