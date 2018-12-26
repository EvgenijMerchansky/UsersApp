using System;
using System.Threading;
using System.Threading.Tasks;
using UsersApp.BLL.Services;

namespace UsersApp.EF.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }

        Task CommitAsync(CancellationToken token);
    }
}
