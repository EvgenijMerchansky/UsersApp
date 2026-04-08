namespace Users.Example.DBLayer.Repositories;

using System.Threading;
using System.Threading.Tasks;
using Users.Example.DBLayer.EntityFramework;
using Users.Example.DBLayer.Repositories.Interfaces;

public class UnitOfWork(UsersContext usersContext, IUserRepository userRepository) : IUnitOfWork
{
    public IUserRepository UserRepository { get; } = userRepository;

    public Task CommitAsync(CancellationToken ct) => usersContext.SaveChangesAsync(ct);

    public void Dispose() => usersContext.Dispose();
}