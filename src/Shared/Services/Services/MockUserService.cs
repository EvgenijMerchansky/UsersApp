using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Users.Example.Models.Dtos.Users;

namespace Users.Example.Services.Services;

public class MockUserService : IMockUserService
{
    private readonly UserDto _mockedUser = new()
    {
        Id = 1,
        FirstName = "John",
        LastName = "Smith",
        Email = "john.smith@email.com"
    };

    public async Task<IEnumerable<UserDto>> GetAll(CancellationToken ct)
    {
        return new List<UserDto> { _mockedUser };
    }

    public async Task<UserDto> Get(int id, CancellationToken ct)
    {
        return _mockedUser;
    }

    public async Task Create(UserDto userDto, CancellationToken ct)
    {
        return;
    }

    public async Task Update(int id, UserDto userDto, CancellationToken ct)
    {
        return;
    }

    public async Task Delete(int id, CancellationToken ct)
    {
        return;
    }
}

public interface IMockUserService
{
    Task<IEnumerable<UserDto>> GetAll(CancellationToken ct = default);
    Task<UserDto> Get(int id, CancellationToken ct = default);
    Task Create(UserDto userDto, CancellationToken ct = default);
    Task Update(int id, UserDto userDto, CancellationToken ct = default);
    Task Delete(int id, CancellationToken ct = default);
}