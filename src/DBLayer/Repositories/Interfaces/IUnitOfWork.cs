using System;
using System.Threading;
using System.Threading.Tasks;

namespace Users.Example.DBLayer.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }

    Task CommitAsync(CancellationToken ct);
}