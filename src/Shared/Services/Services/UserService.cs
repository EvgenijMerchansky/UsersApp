using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Users.Example.DBLayer.Models;
using Users.Example.DBLayer.Repositories.Interfaces;
using Users.Example.Models.Dtos.Users;

namespace Users.Example.Services.Services;

public class UserService(IMapper mapper, IUserRepository userRepository) : IUserService
{
    public async Task<IEnumerable<UserDto>> GetAll(CancellationToken ct)
    {
        var users = await userRepository.GetAll(ct);
        return mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto> Get(int id, CancellationToken ct)
    {
        var user = await userRepository.Get(id, ct);
        return mapper.Map<UserDto>(user);
    }

    public async Task Create(CreateUserDto createUserDto, CancellationToken ct)
    {
        await userRepository.Create(mapper.Map<User>(createUserDto), ct);
        await userRepository.CommitAsync(ct);
    }

    public async Task Update(int id, UserDto userDto, CancellationToken ct)
    {
        userRepository.Update(id, mapper.Map<User>(userDto));
        await userRepository.CommitAsync(ct);
    }

    public async Task Delete(int id, CancellationToken ct)
    {
        await userRepository.Delete(id, ct);
        await userRepository.CommitAsync(ct);
    }
}

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAll(CancellationToken ct = default);
    Task<UserDto> Get(int id, CancellationToken ct = default);
    Task Create(CreateUserDto createUserDto, CancellationToken ct = default);
    Task Update(int id, UserDto userDto, CancellationToken ct = default);
    Task Delete(int id, CancellationToken ct = default);
}