using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Users.Example.DBLayer.Models;
using Users.Example.DBLayer.Repositories.Interfaces;
using Users.Example.Models.Dtos.Users;

namespace Users.Example.Services.Services;

public class UserService(IMapper mapper, IUnitOfWork uow) : IUserService
{
    public async Task<IEnumerable<UserDto>> GetAll(CancellationToken ct)
    {
        var users = await uow.UserRepository.GetAll(ct);
        return mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto> Get(int id, CancellationToken ct)
    {
        var user = await uow.UserRepository.Get(id, ct);
        return mapper.Map<UserDto>(user);
    }

    public async Task Create(UserDto userDto, CancellationToken ct)
    {
        await uow.UserRepository.Create(mapper.Map<User>(userDto), ct);
        await uow.CommitAsync(ct);
    }

    public async Task Update(int id, UserDto userDto, CancellationToken ct)
    {
        uow.UserRepository.Update(id, mapper.Map<User>(userDto));
        await uow.CommitAsync(ct);
    }

    public async Task Delete(int id, CancellationToken ct)
    {
        await uow.UserRepository.Delete(id, ct);
        await uow.CommitAsync(ct);
    }
}

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAll(CancellationToken ct = default);
    Task<UserDto> Get(int id, CancellationToken ct = default);
    Task Create(UserDto userDto, CancellationToken ct = default);
    Task Update(int id, UserDto userDto, CancellationToken ct = default);
    Task Delete(int id, CancellationToken ct = default);
}