using System;
using System.Threading;
using System.Threading.Tasks;

namespace UsersApp.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }

        Task CommitAsync(CancellationToken token);
    }
}
